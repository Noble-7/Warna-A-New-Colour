using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorBehaviour : MonoBehaviour
{
    public AudioClip doorOpenAudio;
    public AudioSource audioSource;

    Animator animator;

    public bool isOpen = false;

    public PlayerBehaviour playerRef;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerRef.hasKey == true)
        {
            isOpen = true;
            playerRef.hasKey = false;
        }

        if (isOpen)
        {
            animator.SetInteger("AnimState", 1);
            audioSource.PlayOneShot(doorOpenAudio);
        }

    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        if (playerRef.kills >= numKillsToOpen)
    //        {
    //            animator.SetInteger("AnimState", 2);
    //            isOpen = false;
    //        }
    //    }
    //}
}
