using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class gameEndScript : MonoBehaviour
{

    public string[] dialogue1;

    //This is the TextMeshPro on the canvas with Diana on it, so NOT the test canvas.
    public TextMeshProUGUI dianalogueRef;

    public Texture[] dianaHead;

    //This is the image of Diana in the canvas.
    public RawImage dianaRef;

    private int index;

    //This is the canvas, NOT the test canvas.
    public Canvas canvasRef;

    private bool Collected = false;

    //This is a reference to the player in the scene.
    public PlayerBehaviour playerRef;
    public Material grey;
    public MeshRenderer playerColour;

    [SerializeField]
    private InputActionReference selectControl;


    public AudioSource audioSource;
    public AudioClip pickupAudio;


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTrigger works");
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("We know it's a player");
            audioSource.PlayOneShot(pickupAudio);
            canvasRef.gameObject.SetActive(true);
            //playerRef = other.gameObject.GetComponent<PlayerBehaviour>();
            playerRef.hasBlue = false;
            playerRef.hasRed = false;
            playerRef.hasGreen = false;
            playerColour.material = grey;
            playerRef.index = 0;
            dianalogueRef.text = dialogue1[index];
            dianaRef.texture = dianaHead[index];
            Collected = true;
            Time.timeScale = 0.0f;
        }
    }

    public void Update()
    {
        if (Collected)
        {
            if (selectControl.action.triggered)
            {


                index++;
                if (index > dialogue1.Length - 1 || index > dianaHead.Length - 1)
                {
                    Time.timeScale = 0.1f;
                    canvasRef.gameObject.SetActive(false);
                    Destroy(gameObject);
                }
                else
                {
                    dianalogueRef.text = dialogue1[index];
                    dianaRef.texture = dianaHead[index];
                }
            }
        }
    }

    //public void onClick()
    //{
    //    index++;
    //    if (index > dialogue1.Length - 1 || index > dianaHead.Length - 1)
    //    {
    //        canvasRef.gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        dianalogueRef.text = dialogue1[index];
    //        dianaRef.texture = dianaHead[index];
    //    }
    //}


}
