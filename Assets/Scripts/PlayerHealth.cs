using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    [Range(1, 1000)]
    private float health;

    private TextMeshProUGUI healthText;

    private DataManager dataManager;
    private Bullet bullet;

    [HideInInspector]
    public bool isDead = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            bullet = other.GetComponent<Bullet>();
            health = health - bullet.bulletDamage;
            healthText.text = "Health: " + Mathf.Round(health);

            if (health <= 0)
            {
                isDead = true;
                healthText.text = "Health: 0";
                Destroy(gameObject);
            }
        }
    }

    private void Start()
    {
        dataManager = GameObject.Find("GameManager").GetComponent<DataManager>();
        healthText = GameObject.Find("HealthText").GetComponent<TextMeshProUGUI>();

        health = health * dataManager.healthMultiplier;
        healthText.text = "Health: " + Mathf.Round(health);
    }
}
