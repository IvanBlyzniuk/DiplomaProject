using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace App.World.SceneObjects.Actuators
{
    public class PressurePlate : BaseActuator
    {
        private int currentUnitsPressing = 0;
        private bool isPressed = false;
        private SpriteRenderer spriteRenderer;
        private Sprite inactiveSprite;

        [SerializeField] private int unitsNeededToPress;
        [SerializeField] private Sprite activeSprite;
        [SerializeField] private TextMeshPro unitsToPressText;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            inactiveSprite = spriteRenderer.sprite;
            updateText();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            //Debug.Log("Collision");
            currentUnitsPressing++;
            updateText();
            if(!isPressed && currentUnitsPressing == unitsNeededToPress)
            {
                Activate();
                spriteRenderer.sprite = activeSprite;
                isPressed = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if(currentUnitsPressing > 0)
                currentUnitsPressing--;
            updateText();
            if(isPressed && currentUnitsPressing < unitsNeededToPress)
            {
                Deactivate();
                spriteRenderer.sprite = inactiveSprite;
                isPressed = false;
            }
        }

        private void updateText()
        {
            int leftToPress = unitsNeededToPress - currentUnitsPressing;
            unitsToPressText.text = (leftToPress >= 0 ? leftToPress : 0).ToString();
            if (leftToPress <= 0)
                unitsToPressText.color = Color.green;
            else if (leftToPress < unitsNeededToPress)
                unitsToPressText.color = Color.yellow;
            else
                unitsToPressText.color = Color.red;
        }
    }
}
