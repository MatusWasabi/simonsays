using UnityEngine;
using UnityEngine.SceneManagement;

namespace SimonSays.Scripts
{
    /// <summary>
    /// Used for manipulating scene. Invoke it from other unity events.
    /// For example, button UI OnClick() unity event.
    /// Attach this to a prefab for reusability. 
    /// </summary>
    public class SceneLoader : MonoBehaviour
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }
    }
}
