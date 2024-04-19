using UnityEngine;

public class Machine : MonoBehaviour
{
    public GameObject ballPrefab; 
    public Transform spawnPoint; 
    public float shootForce; 
    public float shootInterval; 
    public AudioClip shootSound; 

    void Start()
    {
        // Comienza a disparar pelotas con un intervalo de tiempo específico
        InvokeRepeating("ShootBall", shootInterval, shootInterval);
    }

    // Método para disparar la pelota
    void ShootBall()
    {
        GameObject ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(spawnPoint.forward * shootForce, ForceMode.Impulse);
        }

        // Reproducir sonido del disparo
        AudioSource.PlayClipAtPoint(shootSound, spawnPoint.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bate")) 
        {
            Rigidbody ballRigidbody = other.GetComponent<Rigidbody>(); 
            if (ballRigidbody != null)
            {
                // Aplicar una fuerza a la pelota en la dirección opuesta al impacto del bate
                Vector3 forceDirection = transform.position - other.transform.position;
                ballRigidbody.AddForce(forceDirection.normalized * 100f, ForceMode.Impulse);
                ballRigidbody.useGravity = true;

                // Reproducir sonido del impacto
                AudioSource.PlayClipAtPoint(shootSound, transform.position);
            }
        }
    }
}