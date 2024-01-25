using App.World;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace App.Systems
{
    public class InputSystem : MonoBehaviour
    {

        private CameraTarget cameraTarget;
        private UnitSelectionSystem unitSelectionSystem;
        private FlagSystem flagSystem;
        private Camera mainCamera;
        private InputStates inputState;
        private Action<Vector2> lmbDown;
        private Action<Vector2> lmbHold;
        private Action<Vector2> mouseMoved;
        private Action lmbUp;

        [SerializeField] private float edgeSize = 10f;
        [SerializeField] private bool enableEndgePan = true;

        public InputStates InputState
        {
            get => inputState;
            set
            {
                inputState = value;
                switch(inputState)
                {
                    case InputStates.Empty:
                        lmbDown = unitSelectionSystem.OnMousePressed;
                        lmbHold = unitSelectionSystem.OnMouseHold;
                        lmbUp = unitSelectionSystem.OnMouseReleased;
                        mouseMoved = null;
                        break;
                    case InputStates.PlacingFlag:
                        lmbDown = flagSystem.OnMousePressed;
                        mouseMoved = flagSystem.OnMouseMoved;
                        lmbHold = null;
                        lmbUp = null;
                        break;
                }
            }
        }

        public void Init(UnitSelectionSystem unitSelectionSystem, FlagSystem flagSystem, Camera mainCamera, CameraTarget cameraTarget)
        {
            this.unitSelectionSystem = unitSelectionSystem;
            this.flagSystem = flagSystem;
            this.mainCamera = mainCamera;
            this.cameraTarget = cameraTarget;
            InputState = InputStates.Empty;
        }

        void Update()
        {
            HandleCameraMovement();
            HandleMouseInput();
            HandleKeyboardInput();
        }

        private void HandleKeyboardInput()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
                unitSelectionSystem.ShouldAddSelection = true;
            if (Input.GetKeyUp(KeyCode.LeftShift))
                unitSelectionSystem.ShouldAddSelection = false;
        }

        private void HandleMouseInput()
        {
            mouseMoved?.Invoke(Input.mousePosition);
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
                lmbDown?.Invoke(Input.mousePosition); //unitSelectionSystem.OnMousePressed(Input.mousePosition);
            if (Input.GetMouseButton(0))
                lmbHold?.Invoke(Input.mousePosition); //unitSelectionSystem.OnMouseHold(Input.mousePosition);
            if (Input.GetMouseButtonUp(0))
                lmbUp?.Invoke(); //unitSelectionSystem.OnMouseReleased();
        }

        private void HandleCameraMovement()
        {
            float horizontalKeyboardMove = Input.GetAxis("Horizontal");
            float verticalKeyboardMove = Input.GetAxis("Vertical");

            Vector2 keyboardMovingDirection = new Vector2(horizontalKeyboardMove, verticalKeyboardMove).normalized;

            float horizontalMouseMove = 0f;
            float verticalMouseMove = 0f;
            if (Input.mousePosition.x > Screen.width - edgeSize)
                horizontalMouseMove = 1f;
            else if (Input.mousePosition.x < edgeSize)
                horizontalMouseMove = -1f;

            if (Input.mousePosition.y > Screen.height - edgeSize)
                verticalMouseMove = 1f;
            else if (Input.mousePosition.y < edgeSize)
                verticalMouseMove = -1f;

            Vector2 mouseMovingDirection;
            if (enableEndgePan)
                mouseMovingDirection = new Vector2(horizontalMouseMove, verticalMouseMove).normalized;
            else
                mouseMovingDirection = Vector2.zero;

            Vector2 cameraMovementDirection = (mouseMovingDirection + keyboardMovingDirection).normalized;

            cameraTarget.Move(cameraMovementDirection);
        }
    }

    public enum InputStates
    {
        PlacingFlag,
        Empty,
    }
}

