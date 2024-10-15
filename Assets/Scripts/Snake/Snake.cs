using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 direction = Vector2.right;
    private List<Transform> snakeSegments;
    public Transform segmentPrefab;
    public int initialSize = 4;

    private void Start()
    {
        snakeSegments = new List<Transform>();
        ResetState();
    }

    private void Update()
    {
        HandleInput();
        ScreenWrap();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W)) direction = Vector2.up;
        if (Input.GetKeyDown(KeyCode.S)) direction = Vector2.down;
        if (Input.GetKeyDown(KeyCode.A)) direction = Vector2.left;
        if (Input.GetKeyDown(KeyCode.D)) direction = Vector2.right;
    }

    void Move()
    {
        Vector3 prevPos = transform.position;
        transform.Translate(direction * moveSpeed * Time.deltaTime);

        for (int i = 1; i < snakeSegments.Count; i++)
        {
            Vector3 temp = snakeSegments[i].position;
            snakeSegments[i].position = prevPos;
            prevPos = temp;
        }
    }

    void ScreenWrap()
    {
        // Get the camera's bounds in world space
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.transform.position.z));

        Vector3 newPosition = transform.position;

        // Wrap the snake when it exceeds the screen bounds
        if (newPosition.x > max.x) newPosition.x = min.x;
        else if (newPosition.x < min.x) newPosition.x = max.x;
        
        if (newPosition.y > max.y) newPosition.y = min.y;
        else if (newPosition.y < min.y) newPosition.y = max.y;

        transform.position = newPosition;
    }


    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = snakeSegments[snakeSegments.Count - 1].position;
        snakeSegments.Add(segment);
    }

    public void ResetState()
    {
        for (int i = 1; i < snakeSegments.Count; i++)
        {
            Destroy(snakeSegments[i].gameObject);
        }
        snakeSegments.Clear();
        snakeSegments.Add(transform);

        for (int i = 1; i < initialSize; i++)
        {
            Grow();
        }

        transform.position = Vector3.zero;
        direction = Vector2.right;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
            Destroy(other.gameObject);
        }
        else if (other.tag == "Obstacle" || other.tag == "Player")
        {
            ResetState(); 
        }
    }
}
