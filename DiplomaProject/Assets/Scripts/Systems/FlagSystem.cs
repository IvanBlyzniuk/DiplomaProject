using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace App.Systems
{
    public class FlagSystem : MonoBehaviour, IPlacementSystem
    {
        private static readonly Color halfTransparrentYellow = new Color(1f, 0.92f, 0.016f, 0.5f);

        private InputSystem inputSystem;
        private SpriteRenderer preview;
        private GameObject selectedFlag;
        private Camera mainCamera;

        public GameObject ObjectToPLace 
        { 
            get => selectedFlag; 
            set
            {
                selectedFlag = value;
                if (value == null)
                {
                    preview.gameObject.SetActive(false);
                    return;
                }
                SpriteRenderer selectedSpriteRenderer = selectedFlag.GetComponent<SpriteRenderer>();
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

        public void Init(InputSystem inputSystem, SpriteRenderer preview, Camera mainCamera)
        {
            this.inputSystem = inputSystem;
            this.preview = preview;
            this.mainCamera = mainCamera;
            preview.gameObject.SetActive(false);
        }

        public void OnMouseMoved(Vector2 cursorPosition)
        {
            Vector2 cursorWorldPosition = mainCamera.ScreenToWorldPoint(cursorPosition);
            preview.transform.position = cursorWorldPosition;
        }

        public void OnMousePressed(Vector2 cursorPosition)
        {
            preview.gameObject.SetActive(false);
            Vector2 cursorWorldPosition = mainCamera.ScreenToWorldPoint(cursorPosition);
            Debug.Log(cursorWorldPosition);
            inputSystem.InputState = InputStates.Empty;
            Instantiate(selectedFlag, cursorWorldPosition, Quaternion.identity);
            //Flag logick
        }
    }
}
