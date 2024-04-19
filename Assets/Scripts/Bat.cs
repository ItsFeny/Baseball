using UnityEngine;

public class Bat : MonoBehaviour
{
    public float hitForce;
    public AudioClip hitSound; // Sonido del impacto

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pelota")) 
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // Calcula la dirección y la normal de la colisión
                Vector3 hitDirection = (collision.contacts[0].point - transform.position).normalized;
                Vector3 hitNormal = collision.contacts[0].normal;

                // Reproducir sonido del impacto
                AudioSource.PlayClipAtPoint(hitSound, transform.position);
                Debug.Log("El bate tocó la pelota");

                // Aplica fuerza para simular un rebote realista
                ballRigidbody.AddForce(Vector3.Reflect(hitDirection, hitNormal).normalized * hitForce, ForceMode.Impulse);
            }
        }
    }
}
