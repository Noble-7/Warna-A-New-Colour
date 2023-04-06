//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TurretHealthBehaviour : MonoBehaviour
//{

//    [SerializeField]
//    private int currentHealth = 30;

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.CompareTag("Bullet"))
//        {
//            currentHealth -= 10;
//            if(currentHealth <= 0)
//            {
//                this.gameObject.SetActive(false);
//            }
//        }
//    }
//}
