using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    public Transform TopLanePosition;

    [SerializeField]
    public Transform BottomLanePosition;

    //The current lane transform of the car
    public Transform CurrentLane;

    //Flags that indicate which direction we should move
    private bool MoveUp;
    private bool MoveDown;

    //Boolean that indicates if the car is at top position
    //public bool IsAtTopLane;

    //The height of the car
    private float PlayerHeight;

    [SerializeField]
    public SpriteRenderer CarBodyRenderer;

    [SerializeField]
    public SpriteRenderer LeftTireRenderer;

    [SerializeField]
    public SpriteRenderer RightTireRenderer;

    public void Start()
    {
        CurrentLane = BottomLanePosition;
        PlayerHeight = CarBodyRenderer.bounds.size.y;
        transform.position = new Vector3(transform.position.x, BottomLanePosition.position.y + PlayerHeight/2, transform.position.z);        
    }

    void Update()
    {    
        DetectMovementPC();
        DetectMovementMobile();        
    }

    void FixedUpdate()
    {
        if (MoveUp) {
            transform.position = new Vector3(transform.position.x, TopLanePosition.position.y + PlayerHeight / 2, transform.position.z);
            MoveUp = false;
            CurrentLane = TopLanePosition;
            CarBodyRenderer.sortingOrder = 11;
            LeftTireRenderer.sortingOrder = 12;
            RightTireRenderer.sortingOrder = 12;            
        }

        if (MoveDown) {
            transform.position = new Vector3(transform.position.x, BottomLanePosition.position.y + PlayerHeight / 2, transform.position.z);
            MoveDown = false;
            CurrentLane = BottomLanePosition;
            CarBodyRenderer.sortingOrder = 15;
            LeftTireRenderer.sortingOrder = 16;
            RightTireRenderer.sortingOrder = 16;
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
            if (CurrentLane == TopLanePosition)
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
