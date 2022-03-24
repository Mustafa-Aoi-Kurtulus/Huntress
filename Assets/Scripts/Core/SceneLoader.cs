using Huntress.Characters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Huntress.Core
{
    public class SceneLoader : MonoBehaviour
    {
        public bool canChangeScenes;
        [SerializeField] string sceneName;
        PlayerInputActions playerInput;
        private void Awake()
        {
            playerInput = FindObjectOfType<PlayerInputActions>();
        }

        void Update()
        {
            if (canChangeScenes && playerInput.interactionButtonPressed)
            {
                SceneManager.LoadSceneAsync(sceneName);
                playerInput.interactionButtonPressed = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canChangeScenes = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                canChangeScenes = false;
            }
        }

    }
}