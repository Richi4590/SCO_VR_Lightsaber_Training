using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    public GameObject objectToMove;
    public Transform startPosition;
    public Transform endPosition;
    public float speed = 2f;
    public bool shouldDisAppearInstead = false;

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
        if (shouldDisAppearInstead)
            objectToMove.SetActive(true);
        else
            if (isAtEndPosition) 
            {
                isMovingUp = true;
                isMovingDown = false;
            }
    }

    public void MoveDown()
    {
        if (shouldDisAppearInstead)
            objectToMove.SetActive(false);
        else
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
        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, targetPosition.position, speed * Time.deltaTime);

        if (Vector3.Distance(objectToMove.transform.position, targetPosition.position) < 0.01f)
        {
            // Stop movement and set the current state
            objectToMove.transform.position = targetPosition.position;
            isMovingUp = false;
            isMovingDown = false;
            isAtEndPosition = targetPosition == endPosition;
        }
    }
}