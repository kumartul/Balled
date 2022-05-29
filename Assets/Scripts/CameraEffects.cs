using System.Collections;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private float _targetRotation;

    private Vector2 _targetRotRange = new Vector2(5f, 45f);     
    private Vector2 _rotationTime = new Vector2(0.05f, 0.1f);   
    private Vector2 _delayRange = new Vector2(2f, 10f);         

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotation());
    }

    // IEnumerator: Tilts the camera
    IEnumerator Rotate()
    {
        float t = 0;

        yield return new WaitForSeconds(Random.Range(_delayRange.x, _delayRange.y));

        while (t < 2)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, _targetRotation), Random.Range(_rotationTime.x, _rotationTime.y));

            t += Time.fixedDeltaTime;

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }

    // IEnumerator: Rotates the camera
    IEnumerator Rotation()
    {
        while (true)
        {
            _targetRotation = Random.Range(_targetRotRange.x, _targetRotRange.y);
            yield return StartCoroutine(Rotate());

            _targetRotation = 0f;
            yield return StartCoroutine(Rotate());

            _targetRotation = -Random.Range(_targetRotRange.x, _targetRotRange.y);
            yield return StartCoroutine(Rotate());

            _targetRotation = 0f;
            yield return StartCoroutine(Rotate());
        }
    }
}
