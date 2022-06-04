using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _gameOverScoreText;
    [SerializeField] private TextMeshProUGUI _gameOverHighScoreText;
    [SerializeField] private TextMeshProUGUI _countDownText;

    [SerializeField] private PlayerController _playerController;

    [SerializeField] private AudioSource _canvasAudioSource;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _321Go;
    [SerializeField] private AudioClip _clickSound;

    private int _score = 0;
    private int _highScore;

    [SerializeField] private GameObject gameOverPanel;

    private SaveData saveData;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        saveData = new SaveData();
        saveData.LoadHighScore();

        _highScore = saveData.highScore;

        _highScoreText.text = "High Score: " + _highScore;

        _audioSource.PlayOneShot(_321Go);
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    private void Update()
    {
        if(_score > _highScore)
        {
            _highScore = _score;            
            _highScoreText.text = "High Score: " + _highScore;
        }
    }

    // Function: Increases the score by 1 every 100 milliseconds when the player is alive
    private IEnumerator UpdateScore()
    {
        while(!PlayerController.gameOver)
        {
            _score += 1;
            _scoreText.text = "Score: " + _score.ToString();

            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator CountDown()
    {
        for(int i = 3; i > 0; i--)
        {
            _countDownText.text = i.ToString();

            yield return new WaitForSeconds(1f);
        }
        
        _countDownText.text = string.Empty;

        _playerController.Init();

        _audioSource.PlayOneShot(_backgroundMusic);

        StartCoroutine(UpdateScore());
    }

    // Function: If the score is equal to highscore (which means that the player 
    // has beaten the highscore), then the highscore will be updated
    public void UpdateHighScore()
    {
        if(_score >= _highScore)
        {
            saveData.highScore = _highScore;
            saveData.SaveHighScore();
        }
    }

    // Function: Starts the game
    public void RestartGame()
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
        _canvasAudioSource.PlayOneShot(_clickSound);
    }

    // Function: Displays the game over panel and stops the game
    public void GameOver()
    {
        gameOverPanel.SetActive(true);

        _gameOverScoreText.text = "Score: " + _score.ToString();
        _gameOverHighScoreText.text = "High Score: " + _highScore.ToString();

        _audioSource.Stop();
    }

    [System.Serializable]
    class SaveData
    {
        public int highScore;

        // Saves the highscore to a file named 'balled.json'
        public void SaveHighScore()
        {
            SaveData data = new SaveData();
            data.highScore = highScore;

            string json = JsonUtility.ToJson(data);

            File.WriteAllText(Application.persistentDataPath + "/balled.json", json);
        }

        // Loads the highscore if the file exists
        public void LoadHighScore()
        {
            string path = Application.persistentDataPath + "/balled.json";

            if(File.Exists(path))
            {
                string json = File.ReadAllText(path);

                SaveData data = JsonUtility.FromJson<SaveData>(json);

                highScore = data.highScore;
            }
        }
    }
}
