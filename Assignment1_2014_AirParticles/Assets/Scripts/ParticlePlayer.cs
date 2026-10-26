
using UnityEngine;
using UnityEngine.InputSystem;

public class ParticlePlayer : MonoBehaviour
{
    // playermovements
    public float moveSpeed = 12f;

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
}
