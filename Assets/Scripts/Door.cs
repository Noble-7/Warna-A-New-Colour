using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 displacement;

    private Vector3 originPos;

    private List<GameObject> enemies;

    private float interpolateAmount;

    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position;
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    // Update is called once per frame
    void Update()
    {
        int enemyCount = enemies.Count;
        foreach (GameObject enemy in enemies)
        {
            if (enemy == null)
            {
                enemies.Remove(enemy);
                enemyCount--;
                break;
            }
        }

        if (enemyCount <= 0 && transform.position != transform.position + displacement)
        {
            interpolateAmount = Mathf.Clamp01(interpolateAmount += Time.deltaTime);

            //Temp Lerp
            transform.position = Vector3.Lerp(originPos, originPos + displacement, interpolateAmount);

            //Insert Animation call
        }
    }
}
