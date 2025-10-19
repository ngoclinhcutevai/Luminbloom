using UnityEngine;
using UnityEngine.SceneManagement; // You must add this line to use the SceneManager

public class StartGame : MonoBehaviour
{
    // This function can be called from a UI button's OnClick event
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}