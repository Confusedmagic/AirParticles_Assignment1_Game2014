
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    // Starts the game by loading the Gameplay scene.
    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    // Opens the instructions screen.
    public void OpenInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    // Returns the player to the main menu.
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Restarts the game from the beginning.
    public void PlayAgain()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
