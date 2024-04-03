using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace App.World.UI
{
    public class Pauser : MonoBehaviour
    {

        private Animator animator;
        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void Pause()
        {
            //gameObject.SetActive(true);
            animator.SetBool("isVisible", true);
            Time.timeScale = 0f;
        }

        public void Continue()
        {
            //gameObject.SetActive(false);
            animator.SetBool("isVisible", false);
            Time.timeScale = 1f;
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Main Menu");
        }
    }
}
