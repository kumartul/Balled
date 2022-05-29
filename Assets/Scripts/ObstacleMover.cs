using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public static float Speed { get; set; }

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        Speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the obstacle to the left
        transform.position += Vector3.left * Time.deltaTime * Speed;
    }
}
