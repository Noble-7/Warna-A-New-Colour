using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndTrigger : MonoBehaviour
{

    private PlayerBehaviour playerRef;
    void Start()
    {
        playerRef = FindObjectOfType<PlayerBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (playerRef.hasBlue == true && playerRef.hasRed == true && playerRef.hasGreen == true && playerRef.hasPurple == true)
            {
                SceneManager.LoadScene("Game Over!");
            }
        }
    }

}



