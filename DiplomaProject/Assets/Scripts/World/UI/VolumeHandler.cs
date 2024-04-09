using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace App.World.UI
{
    public class VolumeHandler : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI numberText;
        private void Awake()
        {
            if (!PlayerPrefs.HasKey("Volume"))
                PlayerPrefs.SetFloat("Volume", AudioListener.volume);
            else
                AudioListener.volume = PlayerPrefs.GetFloat("Volume");
            numberText.text = Mathf.RoundToInt(AudioListener.volume * 10).ToString();
        }

        public void VolumePlus()
        {
            AudioListener.volume += 0.1f;
            if (AudioListener.volume > 1f)
                AudioListener.volume = 1f;
            PlayerPrefs.SetFloat("Volume", AudioListener.volume);
            numberText.text = Mathf.RoundToInt(AudioListener.volume * 10).ToString();
        }

        public void VolumeMinus()
        {
            AudioListener.volume -= 0.1f;
            if (AudioListener.volume < 0f)
                AudioListener.volume = 0f;
            PlayerPrefs.SetFloat("Volume", AudioListener.volume);
            numberText.text = Mathf.RoundToInt(AudioListener.volume * 10).ToString();
        }
    }
}
