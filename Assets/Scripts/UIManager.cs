using System.Collections;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private int _score = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateScore());
    }

    private IEnumerator UpdateScore()
    {
        while(!PlayerController.gameOver)
        {
            _score += 1;
            _scoreText.text = "Score: " + _score.ToString();

            yield return new WaitForSeconds(0.1f);
        }
    }
}
