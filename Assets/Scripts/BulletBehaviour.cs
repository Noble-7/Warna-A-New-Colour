using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{

    public int damage = 20;



    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player") || !other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }



}
