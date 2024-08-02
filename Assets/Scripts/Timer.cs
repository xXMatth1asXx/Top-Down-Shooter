using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float currentTime;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    [SerializeField]
    private TextMeshProUGUI timerText;

    private void Update()
    {
        if (playerHealth.isDead)
            timerText.text = currentTime.ToString("F2");
        else
        {
            currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("F2");
        }
        
    }
}
