using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Function: Starts the game
    public void StartGame()
    {
        SceneManager.LoadScene(SceneNameManager.MAIN_SCENE);
    } 

    // Function: Quits the game
    public void QuitGame()
    {
        Application.Quit();
    }   
}
