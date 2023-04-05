using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : MonoBehaviour
{

    public int damage = 20;



    private void OnCollisionEnter(Collision other)
    {
        if (!(other.gameObject.CompareTag("Enemy")) &&
            !(other.gameObject.CompareTag("Turret")) &&
            !(other.gameObject.CompareTag("EnemyBullet")))
        {
            Debug.Log("Bullet destroyed because it hit" + other.gameObject.tag);
            Destroy(this.gameObject);
        }
    }



}
