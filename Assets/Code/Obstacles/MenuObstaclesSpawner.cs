using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObstaclesSpawner : MonoBehaviour
{
    [SerializeField]
    public Transform spawnGuide;

    [SerializeField]
    public GameObject carPrefab;

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

    // The latest spawned car
    private GameObject latestObject;

    // The width of the player
    private float playerWidth;

    // The min distance between cars
    private float minDistanceBetweenCars;

    void Start()
    {
        playerWidth = playerSpriteRenderer.bounds.size.x;
        minDistanceBetweenCars = playerWidth * 7f;
    }

    private void FixedUpdate()
    {
        //Don't do anything at the beginning
        if (playerRigidBody.linearVelocity.x < 1.0f) return;

        // Edge case when there is no spawned car yet
        if (latestObject == null) {
            SpawnObstacle(carPrefab, TopLanePosition);
            return;
        }

        // Check distance between last object and spawn point
        int latestCarPositionX = (int)latestObject.transform.position.x;
        int spawnPositionX = (int)spawnGuide.transform.position.x;
        if (spawnPositionX - latestCarPositionX > minDistanceBetweenCars)
        {
            if (latestObject.name == "Car(Clone)")
            {
                SpawnObstacle(rockPrefab, BottomLanePosition);
            }
            else
            {
                SpawnObstacle(carPrefab, TopLanePosition);
            }            
        }
    }

    private void SpawnObstacle(GameObject prefab, Transform lane)
    {
        latestObject = Instantiate(prefab);
        float obstacleHeight = latestObject.GetComponent<SpriteRenderer>().bounds.size.y;
        latestObject.transform.position = new Vector3(spawnGuide.position.x, lane.position.y + obstacleHeight / 2, spawnGuide.position.z);
    }   
}
