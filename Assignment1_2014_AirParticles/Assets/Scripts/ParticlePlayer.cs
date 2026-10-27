using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class ParticlePlayer : MonoBehaviour
{
    // playermovements
    public float moveSpeed = 12f;

    // Player health settings
    public int maxHealth = 3;
    private int currentHealth;

    // Health text shown in the Gameplay UI
    public TMP_Text healthText;

    void Start()
    {
        // Start each game with full health
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        Vector2 screenPosition;

        // Checks if the player is touching the screen
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            screenPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();
        }
        // pc testing
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.isPressed)
        {
            screenPosition = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }

        Vector3 target = Camera.main.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, 10f));

        // had to add this because the z position was being set to 10f, which caused the player to move in the z direction so i just
        // set it to 0f so the player would only move in the x and y direction
        target.z = 0f;

        // moves the player towards the postion
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0)
        {
            return;
        }

        currentHealth = Mathf.Max(currentHealth - damage, 0);
        UpdateHealthUI();

        if (currentHealth == 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = "HP: " + currentHealth + "/" + maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");
        SceneManager.LoadScene("GameOver");
    }
}
