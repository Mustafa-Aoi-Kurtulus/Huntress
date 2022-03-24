using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Huntress.Characters.Player
{
    public class PlayerPersistance : MonoBehaviour
    {
        public static void SaveData(PlayerDataHandler player)
        {
            PlayerPrefs.SetFloat("posX", player.transform.position.x);
            PlayerPrefs.SetFloat("posY", player.transform.position.y);
            PlayerPrefs.SetFloat("posZ", player.transform.position.z);
            PlayerPrefs.SetFloat("rotX", player.transform.localRotation.eulerAngles.x);
            PlayerPrefs.SetFloat("rotY", player.transform.localRotation.eulerAngles.y);
            PlayerPrefs.SetFloat("rotZ", player.transform.localRotation.eulerAngles.z);
            PlayerPrefs.SetInt("health", player.GetComponent<CharacterStats>().health);
            PlayerPrefs.SetString("scene", player.gameObject.scene.name);
        }

        public static PlayerData LoadData()
        {
            float posX = PlayerPrefs.GetFloat("posX");
            float posY = PlayerPrefs.GetFloat("posY");
            float posZ = PlayerPrefs.GetFloat("posZ");
            float rotX = PlayerPrefs.GetFloat("rotX");
            float rotY = PlayerPrefs.GetFloat("rotY");
            float rotZ = PlayerPrefs.GetFloat("rotZ");
            int health = PlayerPrefs.GetInt("health");
            string scene = PlayerPrefs.GetString("scene");

            PlayerData playerData = new PlayerData()
            {
                Position = new Vector3(posX, posY, posZ),
                Rotation = new Vector3(rotX, rotY, rotZ),
                Health = health,
                SavedScene = scene,
            };
            return playerData;
        }
    }
}
