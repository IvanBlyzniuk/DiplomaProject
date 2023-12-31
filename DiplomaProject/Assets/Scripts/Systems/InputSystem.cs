using App.World;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{

    private CameraTarget cameraTarget;

    [SerializeField] private float edgeSize = 10f;
    [SerializeField] private bool enableEndgePan = true;

    public void Init(CameraTarget cameraTarget)
    {
        this.cameraTarget = cameraTarget;
    }

    void Update()
    {
        HandleCameraMovement();
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
        if(enableEndgePan)
            mouseMovingDirection = new Vector2(horizontalMouseMove, verticalMouseMove).normalized;
        else
            mouseMovingDirection = Vector2.zero;

        Vector2 cameraMovementDirection = (mouseMovingDirection + keyboardMovingDirection).normalized;

        cameraTarget.Move(cameraMovementDirection);
    }
}
