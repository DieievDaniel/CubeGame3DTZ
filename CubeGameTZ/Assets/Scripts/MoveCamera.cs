using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float swipeDistance;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minSwipeLength;

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool stopTouch = false;

    private void Update()
    {
        Swipe();
    }

    private void Swipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
                stopTouch = false;
            }
            else if (touch.phase == TouchPhase.Moved && !stopTouch)
            {
                currentTouchPosition = touch.position;
                Vector2 distance = currentTouchPosition - startTouchPosition;

                if (Mathf.Abs(distance.x) > minSwipeLength)
                {
                    if (distance.x < -swipeDistance)
                    {
                        MoveRight();
                        stopTouch = true;
                    }
                    else if (distance.x > swipeDistance)
                    {
                        MoveLeft();
                        stopTouch = true;
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                stopTouch = false;
            }
        }
    }

    private void MoveLeft()
    {
        if (transform.position.x > minX)
        {
            transform.Translate(Vector3.left * swipeDistance);
        }
    }

    private void MoveRight()
    {
        if (transform.position.x < maxX)
        {
            transform.Translate(Vector3.right * swipeDistance);
        }
    }
}