using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    // If the touch is longer than MAX_SWIPE_TIME, we dont consider it a swipe
    private const float MAX_SWIPE_TIME = 0.5f;

    // Factor of the screen width that we consider a swipe
    // 0.17 works well for portrait mode 16:9 phone
    private const float MIN_SWIPE_DISTANCE = 0.17f;

    [SerializeField]
    public Transform _topLanePosition;

    [SerializeField]
    public Transform _bottomLanePosition;

    //Flags that indicate which direction we should move
    private bool MoveUp;
    private bool MoveDown;

    //Swipe start position
    private Vector2 SwipeStartPos;

    //Swipe start time
    private float SwipeStartTime;

    //Boolean that indicates if the car is at top position
    public bool IsAtTopLane;

    public void Start()
    {
        transform.position = new Vector3(transform.position.x, _bottomLanePosition.position.y + 1.5f, transform.position.z);
        IsAtTopLane = false;
    }

    void Update()
    {    
        DetectMovementPC();
        DetectMovementMobile();        
    }

    void FixedUpdate()
    {
        if (MoveUp) {
            transform.position = new Vector3(transform.position.x, _topLanePosition.position.y + 1.5f, transform.position.z);
            MoveUp = false;
            IsAtTopLane = true;
        }

        if (MoveDown) {
            transform.position = new Vector3(transform.position.x, _bottomLanePosition.position.y + 1.5f, transform.position.z);
            MoveDown = false;
            IsAtTopLane = false;
        }
    } 

    private void DetectMovementPC()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveDown = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveUp = true;
        }
    }

    private void DetectMovementMobile()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                SwipeStartPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);
                SwipeStartTime = Time.time;
            }
            if (t.phase == TouchPhase.Ended)
            {
                if (Time.time - SwipeStartTime > MAX_SWIPE_TIME) // press too long
                    return;

                Vector2 endPos = new Vector2(t.position.x / (float)Screen.width, t.position.y / (float)Screen.width);

                Vector2 swipe = new Vector2(endPos.x - SwipeStartPos.x, endPos.y - SwipeStartPos.y);

                if (swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
                    return;

                if (Mathf.Abs(swipe.x) < Mathf.Abs(swipe.y))
                { 
                    if (swipe.y > 0) // Vertical swipe
                    {
                        MoveUp = true;
                    }
                    else
                    {
                        MoveDown = true;
                    }
                }
            }
        }
    }
}
