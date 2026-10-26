using UnityEngine;
using System.Collections.Generic;

public class GoodAirSpawner : MonoBehaviour
{
    // The particle prefab spawner its not just goodair now i just combined it with polluted air so i can just use one script for both spawners
    public GameObject particlePrefab;
    public float spawnInterval = 3f;
    public int maxParticles = 6;

    private List<GameObject> spawnedParticles = new List<GameObject>();

    void Start()
    {
        InvokeRepeating(nameof(SpawnParticle), 1f, spawnInterval);
    }

    void SpawnParticle()
    {
        // w
        spawnedParticles.RemoveAll(particle => particle == null);

        if (particlePrefab == null || spawnedParticles.Count >= maxParticles)
        {
            return;
        }

        // Spawns in Camera view
        Vector3 position = Camera.main.ViewportToWorldPoint(
            new Vector3(
                Random.Range(0.1f, 0.9f),
                Random.Range(0.1f, 0.9f),
                Mathf.Abs(Camera.main.transform.position.z)
            )
        );

        position.z = 0f; // again had to add this so they dont spawn in other z positions

        GameObject particle = Instantiate(particlePrefab, position, Quaternion.identity);
        spawnedParticles.Add(particle);
    }
}
