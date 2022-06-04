using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public static Queue<GameObject> Obstacles;

    public static bool startTimer { get; set; }

    private Vector2 _widthRange = new Vector2(3f, 3f);
    private Vector2 _heightRange = new Vector2(2.3f, 4.3f);

    private Vector3 _startPos;
    private Vector3 _topScale
    {
        get => new Vector3(_topWidth, _topHeight, 1f);
    }

    private Vector3 _bottomScale
    {
        get => new Vector3(_bottomWidth, _bottomHeight, 1f);
    }

    [SerializeField] private Transform _obstacleContainer;

    [SerializeField] private GameObject _obstacle;
    private GameObject _top;
    private GameObject _bottom;
    private GameObject _clone;

    private int _poolSize = 50;

    private float _smooth = 5f;
    private float _topHeight;
    private float _topWidth;
    private float _bottomHeight;
    private float _bottomWidth;
    private float _topInterval
    {
        get => (_topWidth - _smooth / PlayerController.Speed) / PlayerController.Speed;
    }
    private float _bottomInterval
    {
        get => (_bottomWidth - _smooth / PlayerController.Speed) / PlayerController.Speed;
    }

    // Keep a copy of the executing script
    private IEnumerator topRandGenRoutine;
    private IEnumerator bottomRandGenRoutine;

    private bool coroutinesStopped = false;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        _startPos = new Vector3(15f, 0f, 0f);
        
        startTimer = false;

        FillPool();
    }

    // Start is called before the first frame update
    private void Start()
    {
        topRandGenRoutine = TopRandGen();
        bottomRandGenRoutine = BottomRandGen();

        StartCoroutine(topRandGenRoutine);
        StartCoroutine(bottomRandGenRoutine);
    }

    // Update is called once per frame
    private void Update()
    {
        if(startTimer)
        {
            float time = Time.time;

            // Stop all the coroutines if 4 seconds have elapsed and the game is over
            // Ensure that the coroutines stop only once by using a boolean
            if(!coroutinesStopped && (time > 4f) && PlayerController.gameOver)
            {
                coroutinesStopped = true;

                StopCoroutine(topRandGenRoutine);
                StopCoroutine(bottomRandGenRoutine);
            }
        }
    }

    // Function: Instantiates obstacles and enqueues them to the pool of obstacles after
    // deactivating them
    private void FillPool()
    {
        Obstacles = new Queue<GameObject>();

        for (int i = 0; i < _poolSize; i++)
        {
            _clone = Instantiate(_obstacle, _startPos, Quaternion.identity, _obstacleContainer);
            _clone.SetActive(false);

            Obstacles.Enqueue(_clone);
        }
    }

    // Function: Returns a random obstacle from the pool
    private GameObject GetObstacle()
    {
        _clone = Obstacles.Dequeue();
        _clone.transform.position = _startPos;

        return _clone;
    }

    // Function: Updates the width and position of the _top obstacle
    private void UpdateTopTransform()
    {
        _top.transform.localScale = _topScale;
        _top.transform.position = new Vector3(_top.transform.position.x, 5 - _top.transform.localScale.y / 2f, 0f);
    }

    // Function: Updates the width and position of the _bottom obstacle
    private void UpdateBottomTransform()
    {
        _bottom.transform.localScale = _bottomScale;
        _bottom.transform.position = new Vector3(_bottom.transform.position.x, -5 + _bottom.transform.localScale.y / 2f, 0f);
    }

    // Function: Generates _top obstacles at a regular interval
    private IEnumerator TopRandGen()
    {
        _topWidth = _widthRange.x;

        while (true)
        {
            _top = GetObstacle();
            _topHeight = Random.Range(_heightRange.x, _heightRange.y);
            
            UpdateTopTransform();
            
            yield return new WaitForSeconds(_topInterval);
            
            _top.SetActive(true);
        }
    }

    // Function: Generates _bottom obstacles at a regular interval
    private IEnumerator BottomRandGen()
    {
        _bottomWidth = _widthRange.x;

        while (true)
        {
            _bottom = GetObstacle();
            _bottomHeight = Random.Range(_heightRange.x, _heightRange.y);

            UpdateBottomTransform();

            yield return new WaitForSeconds(_bottomInterval);

            _bottom.SetActive(true);
        }
    }
}
