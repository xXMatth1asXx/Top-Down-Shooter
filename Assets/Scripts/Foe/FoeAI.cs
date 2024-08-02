using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeAI : MonoBehaviour
{
    private PlayerHealth playerHealth;

    [SerializeField]
    [Range(0,30)]
    private float speed, rotationSpeed, stoppingDistance, retreatDistance;

    [HideInInspector]
    public Transform player;
    public bool readyToShoot = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (!playerHealth.isDead)
        {
            readyToShoot = false;
            Vector3 transformPosition2d = transform.position;
            Vector3 playerPosition2d = player.position;
            transformPosition2d.y = 6;
            playerPosition2d.y = 6;
            Debug.DrawLine(transform.position, player.position, Color.green);

            Vector3 direction = playerPosition2d - transformPosition2d;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            if (Vector3.Distance(transformPosition2d, playerPosition2d) > stoppingDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                //readyToShoot = true;
            }
            else if (Vector3.Distance(transformPosition2d, playerPosition2d) < stoppingDistance && Vector3.Distance(transformPosition2d, playerPosition2d) > retreatDistance)
            {
                transform.position = transform.position;
                readyToShoot = true;
            }
            else if (Vector3.Distance(transformPosition2d, playerPosition2d) < retreatDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                readyToShoot = true;
            }
        }
    }
}
