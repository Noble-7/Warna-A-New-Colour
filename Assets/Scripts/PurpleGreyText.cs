using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleGreyText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("hasPurple") == 1)
        {
            Destroy(gameObject);
        }
    }

}
