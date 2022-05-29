using System.Collections;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    private float _delay = 6f;
    private float _targetRot = 15f;
    private float _targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rotation());
    }

    // IEnumerator: Tilts the camera
    IEnumerator Rotate()
    {
        float t = 0;

        yield return new WaitForSeconds(_delay);

        while (t < 2)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, _targetRotation), 0.05f);

            t += Time.fixedDeltaTime;

            yield return new WaitForSeconds(Time.fixedDeltaTime);
        }
    }

    // IEnumerator: Rotates the camera
    IEnumerator Rotation()
    {
        while (true)
        {
            _targetRotation = _targetRot;
            yield return StartCoroutine(Rotate());

            _targetRotation = 0f;
            yield return StartCoroutine(Rotate());

            _targetRotation = -_targetRot;
            yield return StartCoroutine(Rotate());

            _targetRotation = 0f;
            yield return StartCoroutine(Rotate());
        }
    }
}
