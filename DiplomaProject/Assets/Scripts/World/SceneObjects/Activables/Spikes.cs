using App.World.Entity.Minion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.World.SceneObjects.Activables
{
    public class Spikes : BaseActivable
    {
        private BoxCollider2D boxCollider;
        private SpriteRenderer spriteRenderer;
        private Sprite inactiveSprite;

        [SerializeField] private Sprite activeSprite;
        [SerializeField] private bool activeByDefault;

        private void Awake()
        {
            boxCollider = GetComponent<BoxCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            inactiveSprite = spriteRenderer.sprite;
            if (!activeByDefault)
            {
                boxCollider.enabled = false;
            }
            else
            {
                spriteRenderer.sprite = activeSprite;
            }
            
        }

        protected override void OnActivate()
        {
            if(activeByDefault)
            {
                boxCollider.enabled = false;
                spriteRenderer.sprite = inactiveSprite;
            }
            else
            {
                boxCollider.enabled = true;
                spriteRenderer.sprite = activeSprite;
            }
        }

        protected override void OnDeactivate()
        {
            if (boxCollider == null) 
                return;
            if (activeByDefault)
            {
                boxCollider.enabled = true;
                spriteRenderer.sprite = activeSprite;
            }
            else
            {
                boxCollider.enabled = false;
                spriteRenderer.sprite = inactiveSprite;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var minion = collision.GetComponent<MinionController>();
            //if (minion == null)
            //    return;
            minion?.Die();
        }
    }
}
