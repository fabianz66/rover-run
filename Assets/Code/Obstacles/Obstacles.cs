using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    public Transform _topLanePosition;

    [SerializeField]
    public Transform _bottomLanePosition;

    [SerializeField]
    public GameObject _conePrefab;
    private Queue<GameObject> _reusableCones = new Queue<GameObject>();
    //private Queue<GameObject> _inScreenCones = new Queue<GameObject>();
    //private int MAX_IN_SCREEN_CONES = 1;

    [SerializeField]
    public GameObject _rockPrefab;
    private Queue<GameObject> _rocks = new Queue<GameObject>();

    [SerializeField]
    public GameObject _carPrefab;
    private Queue<GameObject> ReusableCars = new Queue<GameObject>();
    private Stack<GameObject> CarsInScreen = new Stack<GameObject>();


    
    public Transform _obstaclesSpawnX;

    //public float _timeOfLastObstacleS = 0.0f;

    //This timer includes time since start
    private float timerS = 0.0f;

    //Player information
    [SerializeField]
    public GameObject player;
    private Rigidbody2D playerRigidBody;
    private Transform playerTransform;
    private PlayerControl playerControl;
    private PlayerSpeed playerSpeed;
    private float playerWidthPoints;

    private const float initialCarIntervalS = 5.0f;
    private float currentCarIntervalS = 0.0f;
    private float minCarIntervalS = 0.0f;

    //Car obstacles information
    private float timeSinceLastCarS = 0.0f;

    void Start()
    {
        // Get all player information
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerTransform = player.GetComponent<Transform>();
        playerControl = player.GetComponent<PlayerControl>();
        playerSpeed = player.GetComponent<PlayerSpeed>();
        playerWidthPoints = player.GetComponent<SpriteRenderer>().bounds.size.x;
        currentCarIntervalS = initialCarIntervalS;
        minCarIntervalS = (playerWidthPoints * 1.25f) / playerSpeed.finalMaxVelocity; // How many seconds must pass at current speed to to move playerWidth distance
    }

    public void Update()
    {
        timerS += Time.deltaTime;
    }

    private void FixedUpdate() {
        AddCar();        
    }

    private void AddCar()
    {
        float secondsSinceLastObstacle = timerS - timeSinceLastCarS;

        if (secondsSinceLastObstacle >= initialCarIntervalS)
        {
            AddObstacle(_carPrefab, ReusableCars, _obstaclesSpawnX.position.x, playerTransform.position.y, _obstaclesSpawnX.position.z);
            timeSinceLastCarS = timerS;
        }
    }

    private void AddObstacle(GameObject prefab, Queue<GameObject> reusableQueue, float x, float y, float z)
    {
        GameObject c;
        if (reusableQueue.Count == 0)
        {
            c = Instantiate(prefab);
        } else
        {
            c = reusableQueue.Dequeue();
        }
        c.GetComponent<ReusableObstacle>()._reusableQueue = reusableQueue;
        Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0.0f;
        rb.rotation = 0.0f;
        float prefabWidth = c.GetComponent<SpriteRenderer>().bounds.size.x; //Points
        rb.position = new Vector3(x + prefabWidth/2, y, z);        
    }
}
