using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private float threshold;

    private MousePointer mousePointer;
    private PlayerHealth playerHealth;

    private void Start()
    {
        mousePointer = GameObject.Find("GameManager").GetComponent<MousePointer>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        if (!playerHealth.isDead)
        {
            Vector3 playerPosition = playerHealth.transform.position;
            Vector3 targetPosition = (playerPosition + mousePointer.pointer.position) / 2f;

            targetPosition.x = Mathf.Clamp(targetPosition.x, -threshold + playerPosition.x, threshold + playerPosition.x);
            targetPosition.z = Mathf.Clamp(targetPosition.z, -threshold + playerPosition.z, threshold + playerPosition.z);
            targetPosition.y = 0;
            //print(targetPosition);
            transform.position = targetPosition;
        }
    }
}
