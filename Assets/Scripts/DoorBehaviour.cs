using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public AudioClip doorOpenAudio;
    public AudioSource audioSource;

    Animator animator;

    public bool isOpen = false;

    public PlayerBehaviour playerRef;

    [SerializeField]
    private int numKillsToOpen;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerRef.kills >= numKillsToOpen)
            {
                animator.SetInteger("AnimState", 1);
                isOpen = true;
                audioSource.PlayOneShot(doorOpenAudio);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerRef.kills >= numKillsToOpen)
            {
                animator.SetInteger("AnimState", 2);
                isOpen = false;
            }
        }
    }
}
