using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DianaStation : MonoBehaviour
{

    public string[] dialogue;

    //This is the TextMeshPro on the canvas with Diana on it, so NOT the test canvas.
    public TextMeshProUGUI CanvasTextReference;

    public Texture[] dianaHead;

    //This is the image of Diana in the canvas.
    public RawImage CanvasImageReference;

    private int index;

    //This is the canvas, NOT the test canvas.
    public Canvas canvasRef;

    private bool Interacting = false;

    //This is a reference to the player in the scene.
    public PlayerBehaviour playerRef;
    //public Material grey;
    //public MeshRenderer playerColour;

    [SerializeField]
    private InputActionReference selectControl;

    private bool isInRange = false;

    public AudioSource audioSource;
    public AudioClip pickupAudio;


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnTrigger works");
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Recognized as player, selectControl being enabled");
            selectControl.action.Enable();
            isInRange = true;
            if (selectControl.action.triggered)
            {
                Debug.Log("SelectControl was clicked");
                audioSource.PlayOneShot(pickupAudio);
                canvasRef.gameObject.SetActive(true);
                CanvasTextReference.text = dialogue[index];
                CanvasImageReference.texture = dianaHead[index];
                Interacting = true;
                //Time.timeScale = 0.0f;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            selectControl.action.Disable();
            isInRange = false;
        }
    }

    public void Update()
    {
        if (isInRange)
        {
            if (!Interacting)
            {
                if (selectControl.action.triggered)
                {
                    Debug.Log("SelectControl was clicked");
                    audioSource.PlayOneShot(pickupAudio);
                    canvasRef.gameObject.SetActive(true);
                    CanvasTextReference.text = dialogue[index];
                    CanvasImageReference.texture = dianaHead[index];
                    Interacting = true;
                }

            }
            else {
                if (selectControl.action.triggered)
                {
                    index++;
                    if (index > dialogue.Length - 1 || index > dianaHead.Length - 1)
                    {
                        //Time.timeScale = 0.1f;
                        canvasRef.gameObject.SetActive(false);
                        //Destroy(gameObject);
                        Interacting = false;
                        index = 0;
                    }
                    else
                    {
                        CanvasTextReference.text = dialogue[index];
                        CanvasImageReference.texture = dianaHead[index];
                    }
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
