using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _force = 230f;

    [SerializeField] private Rigidbody2D _rb;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _flapSound;
    [SerializeField] private AudioClip _dieSound;

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

    // If the player hits an obstacle, then game will end
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Obstacle"))
        {
            _audioSource.PlayOneShot(_dieSound);
        }
    }

    // Function: Adds an upward force to the ball
    private void Flap()
    {
        _rb.AddForce(Vector2.up * _force);

        _audioSource.PlayOneShot(_flapSound);
    }
}
