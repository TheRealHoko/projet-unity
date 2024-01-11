using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public float bounceHeight = 20.0f; // Hauteur du saut en mètres

    private void OnCollisionEnter(Collision collision)
    {
        // Vérifie si le joueur a touché le cube
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                // Applique une force verticale pour faire "rebondir" le joueur
                // La formule pour calculer la force nécessaire pour atteindre une certaine hauteur est : sqrt(2 * hauteur * gravité)
                float bounceForce = Mathf.Sqrt(2 * bounceHeight * Physics.gravity.magnitude);
                
                // Applique la force en prenant en compte la gravité pour obtenir un mouvement naturel
                playerRigidbody.AddForce(Vector3.up * bounceForce, ForceMode.VelocityChange);
            }
        }
    }
}