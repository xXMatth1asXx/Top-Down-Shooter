using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foe : MonoBehaviour
{
    [SerializeField]
    [Range(1, 1000)]
    private float health;

    private Bullet bullet;
    private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            bullet = other.GetComponent<Bullet>();
            health = health - bullet.bulletDamage;

            if (health <= 0)
            {
                Destroy(gameObject);
                gameManager.thisGameCoins += 1;
            }
        }
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
}
