using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBehaviour : MonoBehaviour
{
    public PlayerBehaviour playerRef;

    private int hasRed, hasBlue, hasGreen, hasPurple = 0;

    //private void Start()
    //{
        
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            PlayerPrefs.SetFloat("xpos", playerRef.transform.position.x);
            PlayerPrefs.SetFloat("ypos", playerRef.transform.position.y);
            PlayerPrefs.SetFloat("zpos", playerRef.transform.position.z);
            PlayerPrefs.SetInt("kills", playerRef.kills);
            if (playerRef.hasRed)
            {
                hasRed = 1;
                PlayerPrefs.SetInt("hasRed", hasRed);
            }
            else
            {
                PlayerPrefs.SetInt("hasRed", hasRed);
            }

            if (playerRef.hasBlue)
            {
                hasBlue = 1;
                PlayerPrefs.SetInt("hasBlue", hasBlue);
            }
            else
            {
                PlayerPrefs.SetInt("hasBlue", hasBlue);
            }

            if (playerRef.hasGreen)
            {
                hasGreen = 1;
                PlayerPrefs.SetInt("hasGreen", hasGreen);
            }
            else
            {
                PlayerPrefs.SetInt("hasGreen", hasGreen);
            }

            if (playerRef.hasPurple)
            {
                hasPurple = 1;
                PlayerPrefs.SetInt("hasPurple", hasPurple);
            }
            else
            {
                PlayerPrefs.SetInt("hasPurple", hasPurple);
            }

        }
    }
}
