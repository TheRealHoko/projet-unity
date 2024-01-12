using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject gameOverPanel; // Canva affiché lors de la mort du joueur
    public string startMenuScene = "StartMenu"; // Nom de la scène du menu principal
    public string currentLevelScene = "CurrentLevel"; // Nom de la scène du niveau actuel

    private void Start()
    {
        gameOverPanel.SetActive(false); // Désactive le canva de game over
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Si le joueur touche un objet avec le tag "Player"
        {
            TriggerGameOver(); // Déclenche la fonction TriggerGameOver
        }
    }

    private void TriggerGameOver() // Fonction appelée lors de la mort du joueur
    {
        gameOverPanel.SetActive(true); // Active le canva de game over
        
        Cursor.lockState = CursorLockMode.None; // Déverrouille le curseur
        Cursor.visible = true; // Affiche le curseur
    }

    public void ReturnToStartMenu() // Fonction appelée lors du clic sur le bouton "Return to start menu"
    {
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur
        Cursor.visible = false; // Cache le curseur

        SceneManager.LoadScene(startMenuScene); // Charge la scène du menu principal
    }

    public void RestartLevel()
    {
        Cursor.lockState = CursorLockMode.Locked; // Verrouille le curseur
        Cursor.visible = false; // Cache le curseur

        SceneManager.LoadScene(currentLevelScene); // Charge la scène du niveau actuel
    }
}