using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField]
    public Transform _topLaneSpawnArea;

    [SerializeField]
    public Transform _bottomLaneSpawnArea;

    [SerializeField]
    public GameObject _conePrefab;

    [SerializeField]
    public GameObject _rockPrefab;

    [SerializeField]
    public GameObject _carPrefab;

    public float _obstaclesCount;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", 2f, 2f);  //1s delay, repeat every 1s
    }

    void SpawnObstacle() {
        if (_obstaclesCount++ % 2 == 0)
        {
            Instantiate(_rockPrefab,
            new Vector3(_topLaneSpawnArea.position.x, _topLaneSpawnArea.position.y, _topLaneSpawnArea.position.z),
            Quaternion.identity);
        }
        else
        {
            Instantiate(_conePrefab,
            new Vector3(_topLaneSpawnArea.position.x, _topLaneSpawnArea.position.y, _topLaneSpawnArea.position.z),
            Quaternion.identity);
        }
    }    

}
