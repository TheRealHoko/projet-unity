using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public void StartGame() 
    {
        SceneManager.LoadScene("Stage0"); // Charge la scène du jeu
    }

    // Méthode pour quitter le jeu
    public void QuitGame() 
    {
        Application.Quit(); // Quitte le jeu
        
#if UNITY_EDITOR // Si on est dans l'éditeur
        UnityEditor.EditorApplication.isPlaying = false; // Arrête le jeu dans l'éditeur
#endif
    } 
}