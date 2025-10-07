using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    public bool hasPowerup;
    private float powerupStrength = 15.0f;
    public GameObject powerupIndicator;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed); //AddForce: continuously along the direction of the force vector.
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0); // Position the powerup indicator below the player
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with a powerup and destroys the powerup
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true); // Activate the powerup indicator
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7); // Powerup lasts for 7 seconds
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false); // Deactivate the powerup indicator
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player collides with an enemy
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position); // Direction away from the player

            Debug.Log("Player collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse); // Apply a force to the enemy away from the player
        }
    }
}
