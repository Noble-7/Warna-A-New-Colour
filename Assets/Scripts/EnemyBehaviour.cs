using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class EnemyBehaviour : MonoBehaviour
    {
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;
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
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
            currentHealth = maxHealth;
            healthBar.setMaxHealth(maxHealth);
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }


        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged()
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
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
}