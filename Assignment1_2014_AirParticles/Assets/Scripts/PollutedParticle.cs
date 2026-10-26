using UnityEngine;

public class PollutedParticle : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRadius = 6f;

    private Transform player;

    void Start() // Looks for player tag and saves player location
    {
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        float distance = Vector2.Distance(
            transform.position,
            player.position);

        if (distance <= detectionRadius)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.position,
                moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the player's health system
            ParticlePlayer playerScript = other.GetComponent<ParticlePlayer>();

            if (playerScript != null)
            {
                // Each polluted particle hit removes 1 HP
                playerScript.TakeDamage(1);

                // Remove polluted air after it hits the player
                Destroy(gameObject);
            }
        }
    }
}
