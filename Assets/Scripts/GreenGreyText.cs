using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGreyText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("hasGreen") == 1)
        {
            Destroy(gameObject);
        }
    }

}
