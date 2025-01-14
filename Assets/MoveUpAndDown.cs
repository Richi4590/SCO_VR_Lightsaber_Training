using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;   
    public float speed = 2f;       

    private bool isMovingUp = false; 
    private bool isMovingDown = false;
    private bool isAtEndPosition = false;

    private void Start()
    {
        GameManager.Instance().OnGameStart += () => MoveDown();
        GameManager.Instance().OnGameStop += () => MoveUp();
    }

    void Update()
    {

        if (isMovingUp)
        {
            MoveTowardsPosition(startPosition);
        }
        else if (isMovingDown)
        {
            MoveTowardsPosition(endPosition);
        }
    }

    public void MoveUp()
    {
        if (isAtEndPosition) 
        {
            isMovingUp = true;
            isMovingDown = false;
        }
    }

    public void MoveDown()
    {
        if (!isAtEndPosition) 
        {
            isMovingDown = true;
            isMovingUp = false;
        }
    }

    public void ToggleMovement()
    {
        if (!isAtEndPosition)
        {
            MoveDown();
        }
        else
        {
            MoveUp();
        }
    }

    private void MoveTowardsPosition(Transform targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition.position) < 0.01f)
        {
            // Stop movement and set the current state
            transform.position = targetPosition.position;
            isMovingUp = false;
            isMovingDown = false;
            isAtEndPosition = targetPosition == endPosition;
        }
    }
}