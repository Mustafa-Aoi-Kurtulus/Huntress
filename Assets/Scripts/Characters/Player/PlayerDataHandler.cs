using UnityEngine;

namespace Huntress.Characters.Player
{
    public class PlayerDataHandler : MonoBehaviour
    {
        public PlayerData PlayerData { get; private set; }

        [SerializeField] GameObject followCameraTarget;

        public static bool didSceneChange;

        private void Start()
        {
            if (didSceneChange)
            {
                LoadGame();
                didSceneChange = false;
            }
        }
        public void LoadGame()
        {
            PlayerData = PlayerPersistance.LoadData();

            if (DidSceneChange())
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(PlayerData.SavedScene);
                didSceneChange = true;
            }
            else
            {
                transform.position = PlayerData.Position;
                followCameraTarget.transform.rotation = Quaternion.Euler(PlayerData.Rotation);
                GetComponent<CharacterStats>().health = PlayerData.Health;
            }
        }

        bool DidSceneChange()
        {
            if (PlayerData.SavedScene != gameObject.scene.name)
            {
                didSceneChange = true;
                return true;
            }
            else return false;
        }
    }
}