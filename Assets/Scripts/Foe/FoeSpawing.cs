using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoeSpawing : MonoBehaviour
{
    private PlayerHealth playerHealth;

    [SerializeField]
    [Range(0, 50)]
    private float spawnRadius, time;

    [SerializeField]
    [Range(0, 1)]
    private float timeSubstraction, limit;

    public GameObject[] foes;

    private void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        StartCoroutine(SpawnFoe());
    }

    IEnumerator SpawnFoe()
    {
        if (!playerHealth.isDead)
        {
            Vector3 spawnPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            spawnPosition += Random.insideUnitSphere.normalized * spawnRadius;
            spawnPosition.y = 2.1f;
            GameObject spawnedFoe = Instantiate(foes[Random.Range(0, foes.Length)], spawnPosition, Quaternion.identity);
            spawnedFoe.transform.parent = GameObject.Find("Foes").transform;

            if (time <= limit)
                time = limit;
            else
                time -= timeSubstraction;

            yield return new WaitForSeconds(time);
            StartCoroutine(SpawnFoe());
        }
    }
}
