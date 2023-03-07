using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGreyText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("hasRed") == 1)
        {
            Destroy(gameObject);
        }
    }

}
