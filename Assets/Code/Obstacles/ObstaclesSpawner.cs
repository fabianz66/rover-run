using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    [SerializeField]
    public Transform spawnGuide;

    [SerializeField]
    public GameObject carPrefab;

    [SerializeField]
    public GameObject conePrefab;

    [SerializeField]
    public GameObject rockPrefab;

    [SerializeField]
    public GameObject player;

    [SerializeField]
    public Transform playerTransform;

    [SerializeField]
    public Rigidbody2D playerRigidBody;

    [SerializeField]
    public SpriteRenderer playerSpriteRenderer;

    [SerializeField]
    public Transform TopLanePosition;

    [SerializeField]
    public Transform BottomLanePosition;

    //Class that knows in which lane is currently the user
    private PlayerControl playerControl;

    // The latest spawned car
    private GameObject latestObject;

    // The lane of the most recent car
    private Transform latestLane;

    // The width of the player
    private float playerWidth;

    // The min distance between cars
    private float minDistanceBetweenCars;

    void Start()
    {
        playerControl = player.GetComponent<PlayerControl>();
        playerWidth = playerSpriteRenderer.bounds.size.x;
        float playerWidthScale = PlayerPrefs.GetFloat(Constants.KEY_OBSTACLES_DISTANCE, Constants.OBSTACLES_DISTANCE_DIFFICULT);
        minDistanceBetweenCars = playerWidth * playerWidthScale;
    }

    private void FixedUpdate()
    {
        //Don't do anything at the beginning
        if (playerRigidBody.linearVelocity.x < 1.0f) return;

        // Edge case when there is no spawned car yet
        if (latestObject == null) {
            SpawnObstacle(RandomObject(true), playerControl.CurrentLane);
            return;
        }

        // Check distance between last object and spawn point
        int latestCarPositionX = (int)latestObject.transform.position.x;
        int spawnPositionX = (int)spawnGuide.transform.position.x;
        if (spawnPositionX - latestCarPositionX > minDistanceBetweenCars)
        {
            if (latestLane == TopLanePosition) {
                SpawnObstacle(RandomObject(false), BottomLanePosition);
            } else {
                SpawnObstacle(RandomObject(true), TopLanePosition);
            }            
        }
    }

    private void SpawnObstacle(GameObject prefab, Transform lane)
    {
        latestObject = Instantiate(prefab);
        float obstacleHeight = latestObject.GetComponent<SpriteRenderer>().bounds.size.y;
        latestObject.transform.position = new Vector3(spawnGuide.position.x, lane.position.y + obstacleHeight / 2, spawnGuide.position.z);
        latestLane = lane;
    }

    // Not so random anymore.
    // 2 cones cant be next to each other.
    private GameObject RandomObject(bool includeRock)
    {
        int rand = Random.Range(1, 10);
        if (rand > 5) // 6, 7, 8, 9, 10
        {
            return carPrefab;
        }
        else if (rand > 2) // 3, 4 , 5
        {
            if(latestObject != null && latestObject.name == "Cone(Clone)")
            {
                return carPrefab;
            }
            return conePrefab;
        }        
        if (includeRock) // 1, 2
        {
            return rockPrefab;
        }
        return carPrefab;        
    }
}
