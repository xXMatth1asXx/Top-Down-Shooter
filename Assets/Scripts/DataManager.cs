using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataManager : MonoBehaviour
{
    [SerializeField]
    private UpgradeMenu upgradeMenu;

    [SerializeField]
    private TextMeshProUGUI allCoinsText;

    [HideInInspector]
    public bool rifleAcquired = false, sniperAcquired = false;

    [HideInInspector]
    public int weaponEquipped;

    [HideInInspector]
    public float strengthMultiplier, intervallMultiplier, healthMultiplier;

    private void Start()
    {
        WhatAcquired();
        UpdateCoins();
    }

    public void SetWeaponEquipped(int weaponIndex)
    {
        PlayerPrefs.SetInt("WeaponIndex", weaponIndex);
    }

    public int GetWeaponEquipped()
    {
        return PlayerPrefs.GetInt("WeaponIndex", 0);
    }

    public void WhatPurchased(int purchaseIndex)
    {
        /*
        if (purchaseIndex == 0) {
            PlayerPrefs.SetInt("PurchasedRifle", 1);
            rifleAcquired = true;
        }

        if (purchaseIndex == 1)
        {
            PlayerPrefs.SetInt("PurchasedSniper", 1);
            sniperAcquired = true;
        }*/

        switch (purchaseIndex)
        {
            case 0:
                PlayerPrefs.SetInt("PurchasedRifle", 1);
                rifleAcquired = true;
                break;
            case 1:
                PlayerPrefs.SetInt("PurchasedSniper", 1);
                sniperAcquired = true;
                break;
            case 2:
                strengthMultiplier += upgradeMenu.upgradeAdd;
                strengthMultiplier =  Mathf.Round(strengthMultiplier * 100f) / 100f;
                print(strengthMultiplier);
                PlayerPrefs.SetFloat("StrengthMultiplier", strengthMultiplier);
                break;
            case 3:
                intervallMultiplier += upgradeMenu.upgradeAdd;
                intervallMultiplier = Mathf.Round(intervallMultiplier * 100f) / 100f;
                print(intervallMultiplier);
                PlayerPrefs.SetFloat("IntervallMultiplier", intervallMultiplier);
                break;
            case 4:
                healthMultiplier += upgradeMenu.upgradeAdd;
                healthMultiplier = Mathf.Round(healthMultiplier * 100f) / 100f;
                print(healthMultiplier);
                PlayerPrefs.SetFloat("HealthMultiplier", healthMultiplier);
                break;
        }
    }

    public float GetMultiplierStrength()
    {
        return PlayerPrefs.GetFloat("StrengthMultiplier", 1);
    }

    public float GetMultiplierIntervall()
    {
        return PlayerPrefs.GetFloat("IntervallMultiplier", 1);
    }

    public float GetMultiplierHealth()
    {
        return PlayerPrefs.GetFloat("HealthMultiplier", 1);
    }

    private void WhatAcquired()
    {
        if (PlayerPrefs.GetInt("PurchasedRifle", 0) == 1)
        {
            rifleAcquired = true;
        }
        if (PlayerPrefs.GetInt("PurchasedSniper", 0) == 1)
        {
            sniperAcquired = true;
        }

        strengthMultiplier = PlayerPrefs.GetFloat("StrengthMultiplier", 1);
        intervallMultiplier = PlayerPrefs.GetFloat("IntervallMultiplier", 1);
        healthMultiplier = PlayerPrefs.GetFloat("HealthMultiplier", 1);
    }

    public int GetCoins()
    {
        return PlayerPrefs.GetInt("Coins", 0);
    }

    public void AddCoins(int coinsToAdd)
    {
        int coins = PlayerPrefs.GetInt("Coins", 0);
        int allCoins = coins += coinsToAdd;
        PlayerPrefs.SetInt("Coins", allCoins);
        print(allCoins);
    }

    public void UpdateCoins()
    {
        if (allCoinsText != null)
        {
            int allCoins = PlayerPrefs.GetInt("Coins", 0);
            allCoinsText.text = "Coins: " + allCoins;
        }
    }
}
