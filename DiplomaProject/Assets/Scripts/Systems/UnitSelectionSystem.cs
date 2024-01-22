using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Systems
{
    public class UnitSelectionSystem : MonoBehaviour
    {
        private RectTransform selectorImage;
        private Camera mainCamera;
        private Vector2 startPos;
        private Vector2 curPos;
        public void Init(Camera mainCamera, RectTransform selectorImage)
        {
            this.mainCamera = mainCamera;
            this.selectorImage = selectorImage;
        }

        public void OnMousePressed(Vector2 cursorPosition)
        {
            startPos = cursorPosition;
            selectorImage.gameObject.SetActive(true);
        }

        public void OnMouseHold(Vector2 cursorPosition)
        {
            curPos = cursorPosition;
            float width = Mathf.Abs(curPos.x - startPos.x);
            float height = Mathf.Abs(curPos.y - startPos.y);

            Vector2 center = new Vector2((curPos.x + startPos.x - Screen.width) / 2f, (curPos.y + startPos.y - Screen.height) / 2f);

            selectorImage.sizeDelta = new Vector2(width, height);
            selectorImage.localPosition = center;
        }

        public void OnMouseReleased(bool addSelection)
        {
            Vector2 worldStartPos = mainCamera.ScreenToWorldPoint(startPos);
            Vector2 worldCurPos = mainCamera.ScreenToWorldPoint(curPos);

            //Debug.Log($"--------------");

            float width = Mathf.Abs(worldCurPos.x - worldStartPos.x);
            float height = Mathf.Abs(worldCurPos.y - worldStartPos.y);

            Vector2 size = new Vector2(width, height);
            Vector2 center = (worldStartPos + worldCurPos) / 2f;
            selectorImage.gameObject.SetActive(false);
            var selectedMinions = Physics2D.OverlapBoxAll(center, size, 0f, LayerMask.GetMask("Minions"));

            foreach (var minion in selectedMinions)
            {
                //Debug.Log(minion.gameObject.name);
                //if shift is pressed selection += selectedMinions
                //else selection = selectedMinions
            }

        }
    }
}
