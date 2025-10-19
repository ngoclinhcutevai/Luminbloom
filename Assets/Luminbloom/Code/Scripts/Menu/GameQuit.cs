using UnityEngine;

public class GameQuit : MonoBehaviour
{
    // This is the function you will call from the button.
    // It must be 'public' to be visible in the Inspector.
    public void QuitGame()
    {
        // This line quits the application.
        Application.Quit();

        // If you are running the game in the Unity editor, Application.Quit() does nothing.
        // This line is to give you feedback in the console that the button was clicked.
        #if UNITY_EDITOR
        Debug.Log("Quit button was pressed! Application.Quit() is ignored in the Editor.");
        #endif
    }
}