using System.Collections;
using System.IO;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;

    private int _score = 0;
    private int _highScore;

    private SaveData saveData;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        saveData = new SaveData();
        saveData.LoadHighScore();

        _highScore = saveData.highScore;

        _highScoreText.text = "High Score: " + _highScore;
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(UpdateScore());
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

    // Function: If the score is equal to highscore (which means that the player 
    // has beaten the highscore), then the highscore will be updated
    public void UpdateHighScore()
    {
        if(_score == _highScore)
        {
            saveData.highScore = _highScore;
            saveData.SaveHighScore();
        }
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
