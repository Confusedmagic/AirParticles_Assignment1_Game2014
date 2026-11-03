using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class ParticlePlayer : MonoBehaviour
{
    public float moveSpeed = 12f;

    public int maxHealth = 3;
    private int currentHealth;
    public TMP_Text healthText;

    [Header("Optional: stop moving when FIRE is tapped")]
    public RectTransform fireButtonArea;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Update()
    {
        Vector2 screenPosition;

        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            screenPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        }
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.isPressed)
        {
            screenPosition = Mouse.current.position.ReadValue();
        }
        else
        {
            return;
        }

        // Do not drag the player over to the mobile FIRE button when tapped.
        if (fireButtonArea != null)
        {
            Canvas canvas = fireButtonArea.GetComponentInParent<Canvas>();
            Camera uiCamera = canvas != null &&
                              canvas.renderMode != RenderMode.ScreenSpaceOverlay
                ? canvas.worldCamera
                : null;

            if (RectTransformUtility.RectangleContainsScreenPoint(
                fireButtonArea, screenPosition, uiCamera))
            {
                return;
            }
        }

        if (Camera.main == null)
        {
            return;
        }

        Vector3 target = Camera.main.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, 10f));
        target.z = 0f;

        transform.position = Vector3.MoveTowards(
            transform.position, target, moveSpeed * Time.deltaTime);
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
