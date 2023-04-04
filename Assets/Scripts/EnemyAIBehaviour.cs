using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyAIBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;


    public int maxHealth = 50;
    public int damage = 10;
    public PlayerBehaviour playerRef;
    public int currentHealth;

    public HealthBar healthBar;

    public AudioSource audioSource;
    public AudioClip enemyHitAudio;
    public AudioClip enemyDeathAudio;

    private bool isDying = false;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }


    void FixedUpdate()
    {

        int layerMask = 6;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did hit");
            agent.SetDestination(playerRef.transform.position);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20, Color.white);
            Debug.Log("Did not hit");
            //agent.SetDestination(playerRef.transform.position);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Bullet"))
        {
            audioSource.PlayOneShot(enemyHitAudio);
            currentHealth -= other.GetComponent<BulletBehaviour>().damage;
            Destroy(other.gameObject);
            healthBar.setHealth(currentHealth);
            if (currentHealth <= 0.0f && !isDying)
            {
                playerRef.kills++;
                Destroy(this.gameObject, 1.0f);
                isDying = true;
                audioSource.PlayOneShot(enemyDeathAudio);
            }
        }

    }
}
