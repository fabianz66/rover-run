using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    Text _highScoreText;
    float _highScore;

    [SerializeField]
    Text _currentScoreText;
    float _currentScore;

    [SerializeField]
    Text mainText;

    [SerializeField]
    public Transform obstaclesSpawnGuide;

    //Car obstacles
    [SerializeField]
    public GameObject carPrefab;
    private Queue<GameObject> ReusableCars = new Queue<GameObject>();

    //This timer includes time since start
    private float timerS = 0.0f;

    //Player information
    [SerializeField]
    public GameObject player;
    private Rigidbody2D playerRigidBody;
    private Transform playerTransform;
    private AudioSource playerAudio;
    private PlayerControl playerControl;
    private PlayerSpeed playerSpeed;
    private float playerWidthPoints;

    //Interval in seconds of when to spawn a new car
    private const float initialCarIntervalS = 6.0f;
    private float currentCarIntervalS = 0.0f;
    private float minCarIntervalS = 1.0f;
    private float timeOfLastCarS = 0.0f;

    // If the user lost
    private bool gameOver = false;

    private void Start()
    {
        //Initial UI
        mainText.enabled = false;

        // Get all player information
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerAudio = player.GetComponent<AudioSource>();
        playerTransform = player.GetComponent<Transform>();
        playerControl = player.GetComponent<PlayerControl>();
        playerSpeed = player.GetComponent<PlayerSpeed>();
        playerWidthPoints = player.GetComponent<SpriteRenderer>().bounds.size.x;
        currentCarIntervalS = initialCarIntervalS;
        //minCarIntervalS = (playerWidthPoints * 3.0f) / (playerSpeed.finalMaxVelocity); // How many seconds must pass at current speed to to move playerWidth distance
    }

    void Update()
    {
        timerS += Time.deltaTime;
        _currentScore = Mathf.Floor(playerTransform.position.x);
        _currentScore = Mathf.Max(0, _currentScore);
        _currentScoreText.text = "DISTANCIA: " + _currentScore;
        currentCarIntervalS = initialCarIntervalS - 0.5f * (_currentScore / 100.0f);
        currentCarIntervalS = Mathf.Max(currentCarIntervalS, minCarIntervalS);
    }

    private void FixedUpdate()
    {
        AddCar();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenFbImpreza()
    {
        Application.OpenURL("https://www.facebook.com/RepuestosLandRoverImpreza");
    }

    public void PauseResumeGame()
    {
        if (gameOver) return;

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            mainText.enabled = false;
            playerAudio.UnPause();
        }
        else
        {
            Time.timeScale = 0;
            mainText.text = "PAUSA";            
            mainText.enabled = true;
            playerAudio.Pause();
        }
    }

    public void GameOver()
    {
        mainText.text = "FIN DEL VIAJE\nDISTANCIA: " + _currentScore;
        mainText.enabled = true;
        gameOver = true;
        playerAudio.Stop();
        GetComponent<AudioSource>().Play();
        Time.timeScale = 0;
    }

    private void AddCar()
    {
        //Don't do anything at the beginning
        if (playerRigidBody.velocity.x < 1.0f) return;

        float secondsSinceLastObstacle = timerS - timeOfLastCarS;
        if (secondsSinceLastObstacle >= currentCarIntervalS)
        {
            AddObstacle(carPrefab, ReusableCars, obstaclesSpawnGuide.position.x, playerTransform.position.y, obstaclesSpawnGuide.position.z);
            timeOfLastCarS = timerS;
        }
    }

    private void AddObstacle(GameObject prefab, Queue<GameObject> reusableQueue, float x, float y, float z)
    {
        GameObject c;
        if (reusableQueue.Count == 0)
        {
            c = Instantiate(prefab);
        }
        else
        {
            c = reusableQueue.Dequeue();
        }
        c.name = "Car";
        c.GetComponent<ReusableObstacle>()._reusableQueue = reusableQueue;
        Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0.0f;
        rb.rotation = 0.0f;
        float prefabWidth = c.GetComponent<SpriteRenderer>().bounds.size.x; //Points
        rb.position = new Vector3(x, y, z);
    }
}
