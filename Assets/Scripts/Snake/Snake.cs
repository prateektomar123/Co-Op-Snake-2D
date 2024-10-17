using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class Snake : MonoBehaviour
{
    public float moveInterval = 0.1f; // Time between movements (lower = faster)
    public float gridSize = 1f; // Size of each grid cell
    private float moveTimer;

    public Vector2Int direction = Vector2Int.right;
    private List<Transform> segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 4;
    public int Score { get; private set; }
    public bool isPlayerTwo = false;

    public bool TwoPlayerScene;
    // Power-up related variables
    private bool isShieldActive = true;
    private float scoreMultiplier = 1f;
    private float speedMultiplier = 1f;
    private float powerUpCooldown = 3f;
    private bool canUsePowerUp = true;
    public TMP_Text PlayerScore;
    public GameObject GameOverMenu;
    public TMP_Text gameOverText;
    private void Start()
    {
        ResetState();
        StartCoroutine(ActivateShield(5));
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        moveTimer += Time.fixedDeltaTime;
        if (moveTimer >= moveInterval / speedMultiplier)
        {
            moveTimer = 0f;
            Move();
            ScreenWrap();
        }
    }

    void HandleInput()
    {
        if (!isPlayerTwo)
        {
            if (Input.GetKeyDown(KeyCode.W) && direction != Vector2Int.down) direction = Vector2Int.up;
            if (Input.GetKeyDown(KeyCode.S) && direction != Vector2Int.up) direction = Vector2Int.down;
            if (Input.GetKeyDown(KeyCode.A) && direction != Vector2Int.right) direction = Vector2Int.left;
            if (Input.GetKeyDown(KeyCode.D) && direction != Vector2Int.left) direction = Vector2Int.right;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2Int.down) direction = Vector2Int.up;
            if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2Int.up) direction = Vector2Int.down;
            if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2Int.right) direction = Vector2Int.left;
            if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2Int.left) direction = Vector2Int.right;
        }
    }

    void Move()
    {
        // Calculate new head position
        Vector3 newPosition = transform.position + new Vector3(direction.x * gridSize, direction.y * gridSize, 0);

        // Move body segments
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Move the first body segment to the previous position of the head
        if (segments.Count > 0)
        {
            segments[0].position = transform.position;
        }

        // Update head position
        transform.position = newPosition;
    }

    void ScreenWrap()
    {
        Vector3 min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        Vector3 max = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.transform.position.z));

        Vector3 newPosition = transform.position;

        if (newPosition.x > max.x) newPosition.x = min.x;
        else if (newPosition.x < min.x) newPosition.x = max.x;
        
        if (newPosition.y > max.y) newPosition.y = min.y;
        else if (newPosition.y < min.y) newPosition.y = max.y;

        transform.position = newPosition;
    }

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments.Count > 0 
            ? segments[segments.Count - 1].position 
            : transform.position;
        segments.Add(segment);
        
    }

    public void Shrink(int amount = 1)
    {
        for (int i = 0; i < amount && segments.Count > 0; i++)
        {
            Transform lastSegment = segments[segments.Count - 1];
            segments.RemoveAt(segments.Count - 1);
            Destroy(lastSegment.gameObject);
            
        }
    }

    public void ResetState()
    {
        // Clear existing segments
        foreach (Transform segment in segments)
        {
            Destroy(segment.gameObject);
        }
        segments.Clear();

        // Reset head position and direction
        transform.position = new Vector3(
            Mathf.Round(transform.position.x / gridSize) * gridSize,
            Mathf.Round(transform.position.y / gridSize) * gridSize,
            0
        );
        direction = Vector2Int.right;

        // Create initial body segments
        for (int i = 0; i < initialSize - 1; i++)
        {
            Grow();
        }

        // Reset other states
        Score = 0;
        moveTimer = 0f;
        isShieldActive = false;
        scoreMultiplier = 1f;
        speedMultiplier = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MassGainer"))
        {
            Grow();
            UpdateScore(1);
            
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("MassBurner"))
        {
            Shrink();
            UpdateScore(-1);
            
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Finish") || other.CompareTag("Player"))
        {
            if (!isShieldActive)
            {
                Die();
            }
        }
        else if (other.CompareTag("PowerUp"))
        {
            if (canUsePowerUp)
            {
                PowerUp powerUp = other.GetComponent<PowerUp>();
                ActivatePowerUp(powerUp.powerUpType, powerUp.duration);
                Destroy(other.gameObject);
                StartCoroutine(PowerUpCooldown());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Finish") || other.gameObject.CompareTag("Player"))
        {
            if (!isShieldActive)
            {
                Die();
            }
        }
    }
    void Die()
    {
        // Implement death logic here (e.g., game over screen, restart game)
        Debug.Log(this.gameObject.name + " died!");
        if(TwoPlayerScene){
            
            if (isPlayerTwo)
            {
                gameOverText.text = "Snake 1 Wins! Snake 2 Died!";
            }
            else{
                gameOverText.text = "Snake 2 Wins! Snake 1 Died!";
            }
        }
        GameOverMenu.SetActive(true);
        Time.timeScale = 0f;

        //ResetState();
    }

    void UpdateScore(int points)
    {
        Score += Mathf.RoundToInt(points * scoreMultiplier);
        if(!TwoPlayerScene)
        {
            PlayerScore.text = "Score : " + Score; 

        }
        else
        {
            
            PlayerScore.text = this.gameObject.name +" Score : " + Score; 
        }
        if (Score < 0)
        {
            Score = 0;
        }
    }

    void ActivatePowerUp(PowerUp.PowerUpType type, float duration)
    {
        switch (type)
        {
            case PowerUp.PowerUpType.Shield:
                StartCoroutine(ActivateShield(duration));
                break;
            case PowerUp.PowerUpType.ScoreBoost:
                StartCoroutine(ActivateScoreBoost(duration));
                break;
            case PowerUp.PowerUpType.SpeedUp:
                StartCoroutine(ActivateSpeedBoost(duration));
                break;
        }
    }

    IEnumerator ActivateShield(float duration)
    {
        isShieldActive = true;
        yield return new WaitForSeconds(duration);
        isShieldActive = false;
    }

    IEnumerator ActivateScoreBoost(float duration)
    {
        scoreMultiplier = 2f;
        yield return new WaitForSeconds(duration);
        scoreMultiplier = 1f;
    }

    IEnumerator ActivateSpeedBoost(float duration)
    {
        speedMultiplier = 1.5f;
        yield return new WaitForSeconds(duration);
        speedMultiplier = 1f;
    }

    IEnumerator PowerUpCooldown()
    {
        canUsePowerUp = false;
        yield return new WaitForSeconds(powerUpCooldown);
        canUsePowerUp = true;
    }
}