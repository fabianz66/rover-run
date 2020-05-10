using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    public Transform _topLanePosition;

    [SerializeField]
    public Transform _bottomLanePosition;

    //Flags that indicate which direction we should move
    private bool MoveUp;
    private bool MoveDown;

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
        if (Input.touchCount == 0) return;

        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (IsAtTopLane)
            {
                MoveDown = true;
            }
            else
            {
                MoveUp = true;
            }
        }
    }
}
