using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseWallBehaviour : MonoBehaviour
{
    [SerializeField] 
    private PlayerBehaviour playerRef;
    private MeshRenderer wallColour;

    [SerializeField]
    private Material phaseMat, wallMat;


    // Start is called before the first frame update
    void Start()
    {
        wallColour = this.GetComponent<MeshRenderer>();
        playerRef = FindObjectOfType<PlayerBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRef.index == 3)
        {
            wallColour.material = phaseMat;
        }
        else
        {
            wallColour.material = wallMat;
        }
    }
}
