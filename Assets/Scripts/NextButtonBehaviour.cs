using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NextButtonBehaviour : MonoBehaviour
{
    public string[] dialogue1;

    public TextMeshProUGUI dianalogueRef;

    public Texture[] dianaHead;

    public RawImage dianaRef;

    private int index;

    public Canvas canvasRef;

    private void OnEnable()
    {
        //index = 0;
    }

    public void onClick()
    {
        index++;
        if (index > dialogue1.Length - 1 || index > dianaHead.Length - 1)
        {
            canvasRef.gameObject.SetActive(false);
        }
        else
        {
            dianalogueRef.text = dialogue1[index];
            dianaRef.texture = dianaHead[index];
        }
    }
}
