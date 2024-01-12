using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class VictoryPlatform : MonoBehaviour 
{
    public TextMeshProUGUI victoryText; // Texte à afficher quand le joueur gagne
    public string sceneToLoad = "Stage1"; // Nom de la scène à charger quand le joueur gagne

    private void Start()
    {
        victoryText.gameObject.SetActive(false); // On désactive le texte de victoire au début
    }

    private void OnCollisionEnter(Collision collision) // Quand le joueur touche la plateforme de victoire
    {
        if (collision.gameObject.CompareTag("Player")) // Si le joueur touche la plateforme
        {
            StartCoroutine(HandleVictory()); // On lance la coroutine HandleVictory
        }
    }

    private IEnumerator HandleVictory() // Coroutine qui gère la victoire
    {

        victoryText.color = Color.green; // On change la couleur du texte de victoire
        victoryText.text = "Victory"; // On change le texte de victoire
        victoryText.gameObject.SetActive(true); // On active le texte de victoire

        yield return new WaitForSeconds(2); // On attend 2 secondes

        SceneManager.LoadScene(sceneToLoad); // On charge la scène de victoire
    }
}