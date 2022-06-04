using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clickSound;

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

    // Function: Plays the click sound
    public void PlayClickSound()
    {
        _audioSource.PlayOneShot(_clickSound);
    }
}
