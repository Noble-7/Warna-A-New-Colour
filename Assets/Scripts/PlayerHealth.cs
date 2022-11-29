using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthbar;

    public float currentHealth{ get; private set; }

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthbar != null) healthbar.fillAmount = currentHealth / maxHealth;
        else Debug.LogWarning("Healthbar Not Assigned");
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(healthbar != null) healthbar.fillAmount = currentHealth / maxHealth;
    }
}
