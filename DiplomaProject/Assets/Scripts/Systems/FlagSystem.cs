using App.World.Entity.Flags;
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
        private IFlag selectedFlag;
        private Camera mainCamera;
        private UnitSelectionSystem unitSelectionSystem;

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
                selectedFlag = value.GetComponent<IFlag>();
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

        public void Init(InputSystem inputSystem, UnitSelectionSystem unitSelectionSystem, SpriteRenderer preview, Camera mainCamera)
        {
            this.inputSystem = inputSystem;
            this.unitSelectionSystem = unitSelectionSystem;
            this.preview = preview;
            this.mainCamera = mainCamera;
            preview.gameObject.SetActive(false);
        }

        public void TryDeleteFlag(Vector2 cursorPosition)
        {
            Vector2 cursorWorldPosition = mainCamera.ScreenToWorldPoint(cursorPosition);
            var flagCollider = Physics2D.OverlapPoint(cursorWorldPosition, LayerMask.GetMask("Flags"));
            if (flagCollider == null)
                return;
            IFlag flag = flagCollider.gameObject.GetComponent<IFlag>();
            if (flag == null)
            {
                Debug.LogWarning("Trying ot delete flag with no IFlag component");
                return;
            }
            flag.RemoveOrder();
            Destroy(flag.gameObject);
        }

        public void OnMouseMoved(Vector2 cursorPosition)
        {
            if (selectedFlag == null)
                return;
            Vector2 cursorWorldPosition = mainCamera.ScreenToWorldPoint(cursorPosition);
            preview.transform.position = cursorWorldPosition;
            if(!selectedFlag.CheckPlacementValidity(cursorWorldPosition))
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
            if (!selectedFlag.CheckPlacementValidity(cursorWorldPosition))
                return;
            inputSystem.InputState = InputStates.Empty;
            GameObject flagObject = Instantiate(selectedFlag.gameObject, cursorWorldPosition, Quaternion.identity);
            IFlag placedFlag = flagObject.GetComponent<IFlag>();
            placedFlag.AddOrder(unitSelectionSystem.SelectedMinions);
        }
    }
}
