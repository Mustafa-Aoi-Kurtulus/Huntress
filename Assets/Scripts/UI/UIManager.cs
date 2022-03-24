using Huntress.Characters.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huntress.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] PlayerDataHandler playerData;
        private void Awake()
        {
            playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDataHandler>();
        }
        public void WindowStatus(GameObject window)
        {
            if (window.activeInHierarchy) window.SetActive(false);
            else window.SetActive(true);
        }
        public void Save()
        {
            PlayerPersistance.SaveData(playerData);
        }
        public void QuitGame()
        {
            PlayerPersistance.SaveData(playerData);
            Application.Quit();
        }
    }
}
