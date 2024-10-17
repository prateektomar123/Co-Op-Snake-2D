using UnityEngine;
using System.Collections;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] massGainerPrefabs;
    public GameObject[] massBurnerPrefabs;
    public GameObject[] powerUpPrefabs;
    public float spawnInterval = 4f;
    public Vector3 boundsMin, boundsMax;
    public float padding = 1f;

    public float foodLifetime = 10f;
    public float minPowerUpInterval = 10f;
    public float maxPowerUpInterval = 20f;

    private void Start()
    {
        boundsMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        boundsMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.transform.position.z));

        StartCoroutine(SpawnFoodRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnFoodRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnFood();
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minPowerUpInterval, maxPowerUpInterval));
            SpawnPowerUp();
        }
    }

    void SpawnFood()
    {
        Vector3 randomPosition = GetRandomPosition();
        GameObject foodPrefab;

        
        if (Random.value > 0.3f) 
        {
            foodPrefab = massGainerPrefabs[Random.Range(0, massGainerPrefabs.Length)];
        }
        else 
        {
            foodPrefab = massBurnerPrefabs[Random.Range(0, massBurnerPrefabs.Length)];
        }

        GameObject food = Instantiate(foodPrefab, randomPosition, Quaternion.identity);
        StartCoroutine(DestroyAfterLifetime(food));
    }

    void SpawnPowerUp()
    {
        Vector3 randomPosition = GetRandomPosition();
        GameObject powerUpPrefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];
        GameObject powerUp = Instantiate(powerUpPrefab, randomPosition, Quaternion.identity);
        StartCoroutine(DestroyAfterLifetime(powerUp));
    }

    Vector3 GetRandomPosition()
    {
        return new Vector3(
            Random.Range(boundsMin.x + padding, boundsMax.x - padding),
            Random.Range(boundsMin.y + padding, boundsMax.y - padding),
            0
        );
    }

    IEnumerator DestroyAfterLifetime(GameObject obj)
    {
        yield return new WaitForSeconds(foodLifetime);
        if (obj != null)
        {
            Destroy(obj);
        }
    }
}