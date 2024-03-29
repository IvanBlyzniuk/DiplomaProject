using App.World.Entity.Minion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace App.Systems
{
    public class UnitSelectionSystem : MonoBehaviour
    {
        private RectTransform selectorImage;
        private Camera mainCamera;
        private bool isSelecting;
        private Vector2 startPos;
        private Vector2 curPos;
        private HashSet<MinionController> selectedMinions = new HashSet<MinionController>();

        public ISet<MinionController> SelectedMinions => selectedMinions;
        public bool ShouldAddSelection { get; set; }

        public void Init(Camera mainCamera, RectTransform selectorImage)
        {
            this.mainCamera = mainCamera;
            this.selectorImage = selectorImage;
            isSelecting = false;
        }

        public void OnMousePressed(Vector2 cursorPosition)
        {
            isSelecting = true;
            startPos = cursorPosition;
            selectorImage.gameObject.SetActive(true);
        }

        public void OnMouseHold(Vector2 cursorPosition)
        {
            if (!isSelecting) 
                return;
            curPos = cursorPosition;
            float width = Mathf.Abs(curPos.x - startPos.x);
            float height = Mathf.Abs(curPos.y - startPos.y);

            Vector2 center = new Vector2((curPos.x + startPos.x - Screen.width) / 2f, (curPos.y + startPos.y - Screen.height) / 2f);

            selectorImage.sizeDelta = new Vector2(width, height);
            selectorImage.localPosition = center;
        }

        public void OnMouseReleased()
        {
            if (!isSelecting)
                return;
            Vector2 worldStartPos = mainCamera.ScreenToWorldPoint(startPos);
            Vector2 worldCurPos = mainCamera.ScreenToWorldPoint(curPos);

            float width = Mathf.Abs(worldCurPos.x - worldStartPos.x);
            float height = Mathf.Abs(worldCurPos.y - worldStartPos.y);

            Vector2 size = new Vector2(width, height);
            Vector2 center = (worldStartPos + worldCurPos) / 2f;
            selectorImage.gameObject.SetActive(false);
            var minionsToSelect = Physics2D.OverlapBoxAll(center, size, 0f, LayerMask.GetMask("Minions"));

            if(!ShouldAddSelection)
            {
                ClearSelection();
            }
                

            foreach (var minionCollider in minionsToSelect)
            {
                var minion = minionCollider.gameObject.GetComponent<MinionController>();
                if(minion == null)
                {
                    Debug.LogWarning("Selecting object with no MinionController attached");
                    continue;
                }
                if(ShouldAddSelection)
                {
                    if(selectedMinions.Contains(minion))
                    {
                        selectedMinions.Remove(minion);
                        minion.Deselect();
                    }
                    else
                    {
                        selectedMinions.Add(minion);
                        minion.Select();
                    }
                }
                else
                {
                    selectedMinions.Add(minion);
                    minion.Select();
                }
            }
            isSelecting = false;
            //Debug.Log(selectedMinions.Count);
        }

        public void ClearSelection()
        { 
            foreach (var minion in selectedMinions)
            {
                minion?.Deselect();
            }
            selectedMinions.Clear();
        }
    }
}
