using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public static Queue<GameObject> Obstacles;

    private Vector2 _widthRange = new Vector2(3f, 3f);
    private Vector2 _heightRange = new Vector2(2.3f, 4.3f);

    private Vector3 _startPos;
    private Vector3 __topScale
    {
        get => new Vector3(_topWidth, _topHeight, 1f);
    }

    private Vector3 __bottomScale
    {
        get => new Vector3(_bottomWidth, _bottomHeight, 1f);
    }

    [SerializeField] private Transform _obstacleContainer;

    [SerializeField] private GameObject _obstacle;
    private GameObject _top;
    private GameObject _bottom;

    private int _poolSize = 50;

    private float _speed = 10f;
    private float _smooth = 5f;
    private float _topHeight;
    private float _topWidth;
    private float _bottomHeight;
    private float _bottomWidth;
    private float _topInterval
    {
        get => (_topWidth - _smooth / _speed) / _speed;
    }
    private float _bottomInterval
    {
        get => (_bottomWidth - _smooth / _speed) / _speed;
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        _startPos = new Vector3(15f, 0f, 0f);

        FillPool();
    }

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(_TopRandGen());
        StartCoroutine(_BottomRandGen());
    }

    // Function: Instantiates obstacles and enqueues them to the pool of obstacles after
    // deactivating them
    private void FillPool()
    {
        Obstacles = new Queue<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            GameObject clone = Instantiate(_obstacle, _startPos, Quaternion.identity, _obstacleContainer);
            clone.SetActive(false);

            Obstacles.Enqueue(clone);
        }
    }

    // Function: Updates the speed of the obstacles
    private void UpdateSpeed()
    {
        ObstacleMover.Speed = _speed;
    }

    // Function: Returns a random obstacle from the pool
    private GameObject GetObstacle()
    {
        GameObject clone = Obstacles.Dequeue();
        clone.transform.position = _startPos;

        UpdateSpeed();
        
        return clone;
    }

    // Function: Updates the width and position of the _top obstacle
    private void Update_TopTransform()
    {
        _top.transform.localScale = __topScale;
        _top.transform.position = new Vector3(_top.transform.position.x, 5 - _top.transform.localScale.y / 2f, 0f);
    }

    // Function: Updates the width and position of the _bottom obstacle
    private void Update_BottomTransform()
    {
        _bottom.transform.localScale = __bottomScale;
        _bottom.transform.position = new Vector3(_bottom.transform.position.x, -5 + _bottom.transform.localScale.y / 2f, 0f);
    }

    // Function: Generates _top obstacles at a regular interval
    private IEnumerator _TopRandGen()
    {
        _topWidth = _widthRange.x;

        while (true)
        {
            _top = GetObstacle();
            _topHeight = Random.Range(_heightRange.x, _heightRange.y);
            
            Update_TopTransform();
            
            yield return new WaitForSeconds(_topInterval);
            
            _top.SetActive(true);
        }
    }

    // Function: Generates _bottom obstacles at a regular interval
    private IEnumerator _BottomRandGen()
    {
        _bottomWidth = _widthRange.x;

        while (true)
        {
            _bottom = GetObstacle();
            _bottomHeight = Random.Range(_heightRange.x, _heightRange.y);

            Update_BottomTransform();

            yield return new WaitForSeconds(_bottomInterval);

            _bottom.SetActive(true);
        }
    }
}
