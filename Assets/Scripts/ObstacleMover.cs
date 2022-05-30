using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    private Transform _myTransform;     // Cache the Transform component of this object

    // Start is called before the first frame update
    private void Start()
    {
        _myTransform = transform;
    }

    // Update is called once per frame
    private void Update()
    {
        // Move the obstacle to the left
        _myTransform.position += Vector3.left * Time.deltaTime * PlayerController.Speed;
    }
}
