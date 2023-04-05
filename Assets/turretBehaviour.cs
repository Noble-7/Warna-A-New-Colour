using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform tip;

    private float bulletSpeed = 1000f;

    [SerializeField]
    private AudioClip shootingAudio;
    [SerializeField]
    private AudioSource audioSource;

    private bool isShooting = false;

    private PlayerBehaviour playerRef;

    private int currentHealth = 30;

    private void Start()
    {
        playerRef = FindObjectOfType<PlayerBehaviour>();
    }

    private void FixedUpdate()
    {
        if (isShooting)
        {
            transform.LookAt(playerRef.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.LookAt(playerRef.transform.position);
            isShooting = true;
            StartCoroutine(Fire());
        }

        if (other.CompareTag("Bullet"))
        {
            currentHealth -= 10;
            if (currentHealth <= 0)
            {
                Destroy(this);
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        transform.LookAt(playerRef.transform.position);
    //        if (!isShooting)
    //        {
    //            isShooting = true;
    //            StartCoroutine(Fire());
    //        }
    //    }
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.LookAt(transform.position + Vector3.zero);
            isShooting = false;
            StopCoroutine(Fire());
        }
    }

    IEnumerator Fire()
    {
        while (isShooting)
        {
            yield return new WaitForSeconds(3.0f);
            GameObject instBullet = Instantiate(bullet, tip.position, Quaternion.Euler(tip.rotation.eulerAngles)) as GameObject;
            audioSource.PlayOneShot(shootingAudio);
            Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
            instBulletRigidBody.AddForce(tip.forward * bulletSpeed);
            Destroy(instBullet, 7.0f);
        }
    }

}
