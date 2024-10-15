using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject[] foodPrefabs;
    public GameObject[] powerUpPrefabs;
    public float spawnInterval = 4f;
    public Vector3 boundsMin, boundsMax;

    private void Start()
    {
        boundsMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        boundsMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.transform.position.z));

        InvokeRepeating(nameof(SpawnFood), 2f, spawnInterval);
        InvokeRepeating(nameof(SpawnPowerUp), 10f, Random.Range(10f, 20f));
    }

    void SpawnFood()
    {
        Vector3 randomPosition = new Vector3(Random.Range(boundsMin.x + 3, boundsMax.x - 3), Random.Range(boundsMin.y + 3, boundsMax.y - 3), 0);
        Instantiate(foodPrefabs[Random.Range(0, foodPrefabs.Length)], randomPosition, Quaternion.identity);
    }

    void SpawnPowerUp()
    {
        Vector3 randomPosition = new Vector3(Random.Range(boundsMin.x + 3, boundsMax.x - 3), Random.Range(boundsMin.y + 3, boundsMax.y - 3), 0);
        Instantiate(powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)], randomPosition, Quaternion.identity);
    }
}
