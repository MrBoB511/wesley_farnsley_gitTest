using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float speed = 4.0f;
    private int enemyCount;
    private Rigidbody playerRb;
    public GameObject focalPoint;
    private Vector3 projSpawnPos;
    public bool hasPowerup = false;
    public float powerupStrength = 15.0f;
    public GameObject powerupIndicator;
    public GameObject projPrefab;
    private Enemy[] enemies;
    private GameObject currProj;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        powerupIndicator.transform.Rotate(Vector3.up * 75 * Time.deltaTime);
        if (Input.GetKeyDown("space"))
        {
            if (hasPowerup == true)
            {
                FireProjectile();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7); 
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void FireProjectile()
    {
        StartCoroutine(HammerDown());
    }

    IEnumerator HammerDown()
    {
        playerRb.useGravity = false;
        while (transform.position.y < 4)
        {
            transform.Translate(0.0f, 5f * Time.deltaTime, 0.0f, Space.World);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        while(transform.position.y > 0.27)
        {
            transform.Translate(0.0f, -20f * Time.deltaTime, 0.0f, Space.World);
            yield return null;
        }
        playerRb.useGravity = true;
        enemyCount = FindObjectsOfType<Enemy>().Length - 1;
        enemies = FindObjectsOfType<Enemy>();
        projSpawnPos = transform.position;
        for (int i = enemyCount; i >= 0; i--)
        {
            currProj = Instantiate(projPrefab, projSpawnPos, transform.rotation);
            currProj.GetComponent<projectile>().target = enemies[i].gameObject;
            currProj.GetComponent<projectile>().player = gameObject;
        }
        yield return null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = (collision.gameObject.transform.position - collision.gameObject.transform.position);
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup of strength " + powerupStrength + ".");
        }
    }
}
