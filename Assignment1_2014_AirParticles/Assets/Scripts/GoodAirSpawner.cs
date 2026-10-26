
using UnityEngine;

public class GoodAirSpawner : MonoBehaviour
{
    public GameObject goodAirPrefab;
    public float spawnInterval = 3f;
    public int maxGoodAir = 6;

    void Start()
    {
        InvokeRepeating(nameof(SpawnGoodAir), 1f, spawnInterval);
    }

    void SpawnGoodAir()
    {
        if (FindObjectsByType<GoodAir>(
            FindObjectsSortMode.None).Length >= maxGoodAir)
        {
            return;
        }

        Vector3 position = Camera.main.ViewportToWorldPoint(
            new Vector3(
                Random.Range(0.1f, 0.9f),
                Random.Range(0.1f, 0.9f),
                Mathf.Abs(Camera.main.transform.position.z)
            )
        );

        position.z = 0f; // again had to add this since it wouldnt be on z 0

        Instantiate(goodAirPrefab, position, Quaternion.identity);
    }
}
