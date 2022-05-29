using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        // Move the obstacle to the left
        transform.position += Vector3.left * Time.deltaTime * PlayerController.Speed;
    }
}
