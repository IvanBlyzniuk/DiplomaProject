using App.World.Entity.Minion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.World.SceneObjects
{
    public class LevelEndingTrigger : MonoBehaviour
    {
        private int currentUnitsInterracting = 0;
        private float fadeoutTime = 1f;
        private Coroutine changingLevel;
        private Image blackOverlay;
        private ParticleSystemForceField particleForceField;
        private CircleCollider2D circleCollider;

        [SerializeField] private int unitsNeededToEnd;
        [SerializeField] private string nextLevelName;
        [SerializeField] private TextMeshPro unitsToPressText;

        public void Init(Image blackOverlay)
        {
            this.blackOverlay = blackOverlay;
            circleCollider = GetComponent<CircleCollider2D>();
            particleForceField = GetComponent<ParticleSystemForceField>();
            particleForceField.endRange = circleCollider.radius + 0.5f;
            updateText();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var minion = collision.GetComponent<MinionController>();
            if (minion == null)
                return;
            minion.StartParticles();
            currentUnitsInterracting++;
            updateText();
            if (changingLevel == null && currentUnitsInterracting == unitsNeededToEnd)
            {
                changingLevel = StartCoroutine(ChangeLevel());
            }
        }

        private IEnumerator ChangeLevel()
        {
            PlayerPrefs.SetString("currentLevel", nextLevelName);
            float timePassed = 0;
            blackOverlay.gameObject.SetActive(true);
            while (timePassed < fadeoutTime * 2)
            {
                timePassed += Time.deltaTime;
                blackOverlay.color = new Color(0, 0, 0, timePassed / fadeoutTime);
                //hudSystem.BlackOverlayAlpha = timePassed / fadeoutTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }
            SceneManager.LoadScene(nextLevelName);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            var minion = collision.GetComponent<MinionController>();
            if (minion == null)
                return;
            minion.StopParticles();
            if (currentUnitsInterracting > 0)
                currentUnitsInterracting--;
            updateText();
        }

        private void updateText()
        {
            int leftToPress = unitsNeededToEnd - currentUnitsInterracting;
            unitsToPressText.text = (leftToPress >= 0 ? leftToPress : 0).ToString();
            if (leftToPress <= 0)
                unitsToPressText.color = Color.green;
            else if (leftToPress < unitsNeededToEnd)
                unitsToPressText.color = Color.yellow;
            else
                unitsToPressText.color = Color.red;
        }
    }
}
