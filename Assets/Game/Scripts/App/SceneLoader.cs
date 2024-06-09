using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class SceneLoader
    {
        public void LoadScene(int index)
        {
            if (index >= SceneManager.sceneCountInBuildSettings)
            {
                Debug.Log("Scene doesn't exist");
                return;
            }
            
            if (SceneManager.GetActiveScene().buildIndex != index)
            {
                SceneManager.LoadScene(index);
            }
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}