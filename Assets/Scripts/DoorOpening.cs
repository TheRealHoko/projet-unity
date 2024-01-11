using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Pour IEnumerator
using TMPro; // Pour textmeshpro

public class DoorOpening : MonoBehaviour
{
    public GameObject door; // Référence à la porte
    public float openHeight = 5.0f; // Hauteur de la porte ouverte
    public float openSpeed = 2.0f; // Vitesse d'ouverture de la porte
    public TextMeshProUGUI pressEText; // Texte "Appuyez sur E pour ouvrir la porte"

    private bool playerInRange = false; // Vérifie si le joueur est dans la portée de la porte
    private bool isOpening = false; // Vérifie si la porte est en train de s'ouvrir

    void Start()
    {
        pressEText.gameObject.SetActive(false); // Désactive le texte "Appuyez sur E pour ouvrir la porte"
    }
    
    void Awake()
    {
        pressEText.gameObject.SetActive(false); // Désactive le texte "Appuyez sur E pour ouvrir la porte"
    }

    void Update()
    {
        // Vérifie si le joueur regarde le cube et se trouve dans la portée
        RaycastHit hit; // Variable pour stocker les informations sur l'objet touché
        bool isDoorInSight = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f); // Raycast avec une portée de 5 metres
        bool isPlayerCloseEnough = isDoorInSight && Vector3.Distance(hit.transform.position, Camera.main.transform.position) <= 5f; // Vérifiez la distance de 5 mètres
        
        if (isPlayerCloseEnough && hit.collider.gameObject == door && !isOpening) // Si le joueur regarde la porte et se trouve dans la portée
        {
            pressEText.gameObject.SetActive(true); // Active le texte "Appuyez sur E pour ouvrir la porte"
            if (Input.GetKeyDown(KeyCode.E)) // Si le joueur appuie sur la touche E
            {
                StartCoroutine(OpenDoor()); // Lance la coroutine OpenDoor
            }
        }
        else
        {
            pressEText.gameObject.SetActive(false); // Désactive le texte "Appuyez sur E pour ouvrir la porte"
        }
    }

    IEnumerator OpenDoor() // Coroutine pour ouvrir la porte
    {
        isOpening = true; // La porte est en train de s'ouvrir
        Vector3 closedPosition = door.transform.position; // Position de la porte fermée
        Vector3 openPosition = new Vector3(closedPosition.x, closedPosition.y + openHeight, closedPosition.z); // Position de la porte ouverte
        float elapsedTime = 0; // Temps écoulé depuis le début de la coroutine

        while (elapsedTime < openSpeed) // Tant que le temps écoulé est inférieur à la vitesse d'ouverture
        {
            // Déplace la porte de closedPosition à openPosition en fonction du temps écoulé
            door.transform.position = Vector3.Lerp(closedPosition, openPosition, (elapsedTime / openSpeed));
            elapsedTime += Time.deltaTime; // Ajoute le temps écoulé depuis la dernière frame
            yield return null; // Attend la prochaine frame
        }

        door.transform.position = openPosition; // Place la porte à la position ouverte
        isOpening = false; // La porte n'est plus en train de s'ouvrir
    }
}