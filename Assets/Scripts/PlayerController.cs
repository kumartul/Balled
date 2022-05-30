using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool gameOver { get; private set; }

    public static float Speed { get; set; }
    
    private float _force = 230f;

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _flapSound;
    [SerializeField] private AudioClip _dieSound;

    [SerializeField] private UIManager _uiManager;

    // Start is called before the first frame update
    private void Start()
    {
        gameOver = false;
        Speed = 10f;

        StartCoroutine(IncreaseSpeed());
    }

    // Update is called once per frame
    private void Update()
    {
        // If the user presses the spacebar or the left mouse button, then add an 
        // upward force to the ball
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Flap();
        }
    }

    // Function: Adds an upward force to the ball
    private void Flap()
    {
        _rb.AddForce(Vector2.up * _force);

        _audioSource.PlayOneShot(_flapSound);
    }

    // Function: Increase the speed by 0.5 every 10 seconds
    private IEnumerator IncreaseSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            Speed += 0.5f;
        }
    }

    // If the player hits an obstacle, then game will end
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Obstacle"))
        {
            _uiManager.UpdateHighScore();

            gameOver = true;

            _audioSource.PlayOneShot(_dieSound);

            _uiManager.GameOver();
        }
    }
}
