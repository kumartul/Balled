using UnityEngine;

public class Boundary : MonoBehaviour
{
    // When the obstacle exits the screen (we have used Exit here because we want to 
    // destroy the obstacle when it exits the screen and the collider's size is equal to 
    // that of the screen), then destroy it and add it to the queue so that it can be
    // respawned the next time
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag(TagManager.OBSTACLE))
        {
            other.gameObject.SetActive(false);

            ObstacleGenerator.Obstacles.Enqueue(other.gameObject);
        }
        else 
        {
            Destroy(other.gameObject);
        }
    }
}
