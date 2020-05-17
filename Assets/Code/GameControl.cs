using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField]
    Text TitleText;

    [SerializeField]
    Text SubtitleText;

    [SerializeField]
    public Transform obstaclesSpawnGuide;

    [SerializeField]
    public Button MuteUnmuteButton;
    [SerializeField]
    public Sprite MutedSprite;
    [SerializeField]
    public Sprite UnmutedSprite;

    [SerializeField]
    public Button PauseResumeButton;
    [SerializeField]
    public Sprite PausedSprite;
    [SerializeField]
    public Sprite ResumedSprite;

    //Car obstacles
    [SerializeField]
    public GameObject carPrefab;
    public GameObject conePrefab;
    public GameObject rockPrefab;
    private Queue<GameObject> ReusableCars = new Queue<GameObject>();

    //This timer includes time since start
    private float timerS = 0.0f;

    //Camera
    //public GameObject camera;
    //private AudioSource cameraAudio;

    //Player information
    [SerializeField]
    public GameObject player;
    private Rigidbody2D playerRigidBody;
    private Transform playerTransform;
    private AudioSource playerAudio;
    //private PlayerControl playerControl;
    //private PlayerSpeed playerSpeed;

    //Interval in seconds of when to spawn a new car
    private const float initialCarIntervalS = 6.0f;
    private const float minCarIntervalS = 1.25f;
    private float currentCarIntervalS = 0.0f;    
    private float timeOfLastCarS = 0.0f;

    // If the user lost
    private bool gameOver = false;

    private void Start()
    {
        //Initial UI
        TitleText.enabled = false;
        SubtitleText.enabled = false;
        if (AudioListener.volume == 0) {
            MuteUnmuteButton.image.sprite = MutedSprite;
        } else {
            MuteUnmuteButton.image.sprite = UnmutedSprite;
        }        

        // Get all player information
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerAudio = player.GetComponent<AudioSource>();
        playerTransform = player.GetComponent<Transform>();
        //playerControl = player.GetComponent<PlayerControl>();
        //playerSpeed = player.GetComponent<PlayerSpeed>();
        currentCarIntervalS = initialCarIntervalS;

        //Resume time
        Time.timeScale = 1.0f;

        // Show message
        //StartCoroutine(ShowSubtitleText("Bombeelo!", 2.0f));
    }

    void Update()
    {
        timerS += Time.deltaTime;
        currentCarIntervalS = initialCarIntervalS - 0.5f * (playerTransform.position.x / 100.0f);
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
            TitleText.enabled = false;
            playerAudio.UnPause();
            PauseResumeButton.image.sprite = ResumedSprite;
        }
        else
        {
            Time.timeScale = 0;
            TitleText.text = "PAUSA";            
            TitleText.enabled = true;
            PauseResumeButton.image.sprite = PausedSprite;
            playerAudio.Pause();
        }
    }

    public void MuteUnmute()
    {        
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            MuteUnmuteButton.image.sprite = UnmutedSprite;
        }
        else
        {
            AudioListener.volume = 0;
            MuteUnmuteButton.image.sprite = MutedSprite;
        }
    }

    public void GameOver()
    {
        TitleText.text = "¡PERDISTE!\nDISTANCIA RECORRIDA: " + (int)playerTransform.position.x;
        TitleText.enabled = true;
        SubtitleText.enabled = false;
        gameOver = true;
        playerAudio.Stop();
        Camera.main.GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();        
        Time.timeScale = 0.0f;
    }

    public void GameCompleted()
    {
        TitleText.text = "¡LLEGASTE!\nDISTANCIA RECORRIDA: " + (int)playerTransform.position.x;
        TitleText.enabled = true;
        SubtitleText.enabled = false;
        gameOver = true;
        playerAudio.Stop();
        Camera.main.GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().Play();
        Time.timeScale = 0.0f;
    }

    IEnumerator ShowSubtitleText(string text, float delay, float duration = 5.0f)
    {
        yield return new WaitForSeconds(delay);
        SubtitleText.text = text;
        SubtitleText.enabled = true;
        yield return new WaitForSeconds(duration);
        SubtitleText.enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void AddCar()
    {
        //Don't do anything at the beginning
        if (playerRigidBody.velocity.x < 1.0f) return;

        float secondsSinceLastCar = timerS - timeOfLastCarS;
        if (secondsSinceLastCar >= currentCarIntervalS)
        {
            //int rand = Random.Range(0, 3);
            //if (rand == 0)
            //{
            //    AddObstacle(carPrefab, ReusableCars, obstaclesSpawnGuide.position.x, playerTransform.position.y, obstaclesSpawnGuide.position.z, "Car");
            //}
            //else if (rand == 1)
            //{
            //    AddObstacle(conePrefab, ReusableCars, obstaclesSpawnGuide.position.x, playerTransform.position.y, obstaclesSpawnGuide.position.z, "Cone");
            //}
            //else {
            //    AddObstacle(rockPrefab, ReusableCars, obstaclesSpawnGuide.position.x, playerTransform.position.y, obstaclesSpawnGuide.position.z, "Rock");
            //}
            AddObstacle(carPrefab, ReusableCars, obstaclesSpawnGuide.position.x, playerTransform.position.y, obstaclesSpawnGuide.position.z, "Car");
            timeOfLastCarS = timerS;
        }
    }

    private void AddObstacle(GameObject prefab, Queue<GameObject> reusableQueue, float x, float y, float z, string name)
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
        c.name = name;
        c.GetComponent<ReusableObstacle>()._reusableQueue = reusableQueue;
        Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0.0f;
        rb.rotation = 0.0f;
        float prefabWidth = c.GetComponent<SpriteRenderer>().bounds.size.x; //Points
        rb.position = new Vector3(x, y, z);
    }
}
