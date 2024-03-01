using App.World.Entity.Flags;
using App.World.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace App.Systems
{
    public class FlagSystem : MonoBehaviour, IPlacementSystem
    {
        private static readonly Color halfTransparrentYellow = new Color(1f, 0.92f, 0.016f, 0.5f);
        private static readonly Color halfTransparrentRed = new Color(1f, 0f, 0f, 0.5f);
        
        private InputSystem inputSystem;
        private SpriteRenderer preview;
        private BaseFlag selectedFlag;
        private Camera mainCamera;
        private UnitSelectionSystem unitSelectionSystem;
        private List<FlagSelector> flagSelectors;

        public FlagSelector ActiveSelector { get; set; }


        public GameObject ObjectToPLace 
        { 
            get => selectedFlag.gameObject; 
            set
            {
                if (inputSystem.InputState == InputStates.Playing)
                    return;
                if (value == null)
                {
                    preview.gameObject.SetActive(false);
                    return;
                }
                selectedFlag = value.GetComponent<BaseFlag>();
                SpriteRenderer selectedSpriteRenderer = selectedFlag.gameObject.GetComponent<SpriteRenderer>();
                if(selectedSpriteRenderer == null)
                {
                    Debug.Log("Selected building doesn't contain SpriteRenderer");
                    preview.gameObject.SetActive(false);
                    return;
                }
                inputSystem.InputState = InputStates.PlacingFlag;
                preview.gameObject.SetActive(true);
                preview.sprite = selectedSpriteRenderer.sprite;
                preview.color = halfTransparrentYellow;
            }
        }

        public void Init(InputSystem inputSystem, UnitSelectionSystem unitSelectionSystem, SpriteRenderer preview, Camera mainCamera, List<FlagSelector> flagSelectors)
        {
            this.inputSystem = inputSystem;
            this.unitSelectionSystem = unitSelectionSystem;
            this.preview = preview;
            this.mainCamera = mainCamera;
            this.flagSelectors = flagSelectors;

            foreach (var flagSelector in flagSelectors)
            {
                flagSelector.Init(this);
            }

            preview.gameObject.SetActive(false);
        }

        public void TryDeleteFlag(Vector2 cursorPosition)
        {
            Vector2 cursorWorldPosition = mainCamera.ScreenToWorldPoint(cursorPosition);
            var flagCollider = Physics2D.OverlapPoint(cursorWorldPosition, LayerMask.GetMask("Flags"));
            if (flagCollider == null)
                return;
            BaseFlag flag = flagCollider.gameObject.GetComponent<BaseFlag>();
            if (flag == null)
            {
                Debug.LogWarning("Trying ot delete flag with no IFlag component");
                return;
            }
            flag.RemoveFlag();
            //Destroy(flag.gameObject);
        }

        public void OnMouseMoved(Vector2 cursorPosition)
        {
            if (selectedFlag == null)
                return;
            Vector2 cursorWorldPosition = mainCamera.ScreenToWorldPoint(cursorPosition);
            preview.transform.position = cursorWorldPosition;
            if(!selectedFlag.CheckPlacementValidity(cursorWorldPosition) || ActiveSelector.AllowedFlagsCount == 0)
            {
                preview.color = halfTransparrentRed;
            }
            else
            {
                preview.color = halfTransparrentYellow;
            }
        }

        public void OnMousePressed(Vector2 cursorPosition)
        {
            preview.gameObject.SetActive(false);
            Vector2 cursorWorldPosition = mainCamera.ScreenToWorldPoint(cursorPosition);
            if (!selectedFlag.CheckPlacementValidity(cursorWorldPosition) || ActiveSelector.AllowedFlagsCount == 0)
                return;
            inputSystem.InputState = InputStates.Empty;
            //ActiveSelector.AllowedFlagsCount--;
            GameObject flagObject = Instantiate(selectedFlag.gameObject, cursorWorldPosition, Quaternion.identity);
            BaseFlag placedFlag = flagObject.GetComponent<BaseFlag>();
            placedFlag.Init(unitSelectionSystem.SelectedMinions, ActiveSelector);
        }
    }
}
