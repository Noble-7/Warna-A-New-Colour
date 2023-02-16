using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CollectPurple : MonoBehaviour
{
    public string[] dialogue1;

    public TextMeshProUGUI dianalogueRef;

    public Texture[] dianaHead;

    public RawImage dianaRef;

    private int index;

    public Canvas canvasRef;

    private bool Collected = false;

    public PlayerBehaviour playerRef;

    [SerializeField]
    private InputActionReference selectControl;

    public Text notCollectedText;

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
            playerRef.hasPurple = true;
            index = 0;
            dianalogueRef.text = dialogue1[index];
            dianaRef.texture = dianaHead[index];
            Collected = true;
            Time.timeScale = 0.0f;
            notCollectedText.gameObject.SetActive(false);
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
