using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueGreyText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("hasBlue") == 1)
        {
            Destroy(gameObject);
        }
    }

}
