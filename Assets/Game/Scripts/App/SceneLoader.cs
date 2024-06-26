using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class SceneLoader
    {
        public bool TryLoadScene(int index)
        {
            if (index >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("Scene doesn't exist");
                return false;
            }
            
            if (SceneManager.GetActiveScene().buildIndex != index)
            {
                Debug.Log("LoadScene");
                SceneManager.LoadScene(index);
                return true;
            }

            return false;
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}