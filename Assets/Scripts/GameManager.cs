using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] PlayerTypes;

    private PlayerHealth playerHealth;
    private DataManager dataManager;

    [HideInInspector]
    public int thisGameCoins;

    [SerializeField]
    private TextMeshProUGUI thisGameCoinsText;

    private bool saved = false;

    private void Awake()
    {
        dataManager = gameObject.GetComponent<DataManager>();
        WeaponEquipped();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    } 

    private void Update()
    {
        if (playerHealth.isDead)
        {
            if (!saved)
            {
                dataManager.AddCoins(thisGameCoins);
                saved = true;
            }
        }

        thisGameCoinsText.text = "Coins: " + thisGameCoins;
    }

    private void WeaponEquipped()
    {
        int weaponIndex = dataManager.GetWeaponEquipped();
        GameObject player = Instantiate(PlayerTypes[weaponIndex], new Vector3(0, 2.1f, 0), Quaternion.identity);
        player.transform.parent = GameObject.Find("Player").transform;
    }
}
 