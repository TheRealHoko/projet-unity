using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    public float maxDistance = 10f; // Distance à laquelle le joueur peut interagir avec l'objet
    public LayerMask interactableLayer; // Layer sur lequel se trouve l'objet
    
    private Camera playerCamera; // Caméra du joueur
    void Start()
    {
        playerCamera = Camera.main; // Récupération de la caméra du joueur
    }
    
    void Update() // Vérification de l'activation du bouton
    {
        if (Input.GetMouseButtonDown(0)) // Si le joueur clique
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition); // Création d'un rayon depuis la caméra jusqu'à la position de la souris
            RaycastHit hit; // Variable qui va stocker les informations du rayon

            if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer)) // Si le rayon touche un objet sur le layer interactableLayer
            {
                if (hit.collider.gameObject == gameObject) // Si l'objet touché est le bouton
                {
                    ButtonActivation(); // Activation du bouton
                }
            }
        }
    }
        
    private void ButtonActivation() // Fonction d'activation du bouton
    {
        Debug.Log("Button activated!"); // Affichage d'un message dans la console
    }
}