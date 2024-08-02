using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    private DataManager dataManager;

    [SerializeField]
    private TextMeshProUGUI rifleAcquiredText, sniperAcquiredText, strengthText, intervallText, healthText;

    [SerializeField]
    [Range(0, 1000f)]
    private int riflePrice, sniperPrice, strengthPrice, intervallPrice, healthPrice;

    [SerializeField]
    [Range(1, 50f)]
    private int maxUpgrade;

    [Range(0, 0.5f)]
    public float upgradeAdd;

    public void SelectPistol()
    {
        dataManager.SetWeaponEquipped(0);
        print("1");
    }
    public void SelectRifle()
    {
        if (dataManager.rifleAcquired)
        {
            dataManager.SetWeaponEquipped(1);
            print("2");
        }
        else
        {
            if (dataManager.GetCoins() >= riflePrice)
            {
                dataManager.AddCoins(-riflePrice);
                dataManager.WhatPurchased(0);
                dataManager.SetWeaponEquipped(1);
                dataManager.UpdateCoins();
                rifleAcquiredText.text = "Purchased";
            }
        }
    }

    public void SelectSniper()
    {
        if (dataManager.sniperAcquired)
        {
            dataManager.SetWeaponEquipped(2);
            print("3");
        }
        else
        {
            if (dataManager.GetCoins() >= sniperPrice)
            {
                dataManager.AddCoins(-sniperPrice);
                dataManager.WhatPurchased(1);
                dataManager.SetWeaponEquipped(2);
                dataManager.UpdateCoins();
                sniperAcquiredText.text = "Purchased";
            }
        }
    }

    public void SelectStrength()
    {
        if (dataManager.strengthMultiplier <= 1 + upgradeAdd * maxUpgrade)
        {
            if (dataManager.GetCoins() >= strengthPrice * dataManager.strengthMultiplier)
            {
                dataManager.AddCoins(-(int)Mathf.Round(strengthPrice * dataManager.strengthMultiplier));
                dataManager.WhatPurchased(2);
                dataManager.UpdateCoins();
                strengthText.text ="Level: " + Mathf.Round((dataManager.strengthMultiplier - 1) / upgradeAdd + 1) + " Multiplier: " + dataManager.strengthMultiplier + "\nCoins: " + strengthPrice * dataManager.strengthMultiplier;
            }
        }
        else
        {
            strengthText.text = "Maxed! Multiplier: " + dataManager.strengthMultiplier;
        }
    }
    public void SelectIntervall()
    {
        if (dataManager.intervallMultiplier <= 1 + upgradeAdd * maxUpgrade)
        {
            if (dataManager.GetCoins() >= intervallPrice * dataManager.intervallMultiplier)
            {
                dataManager.AddCoins(-(int)Mathf.Round(intervallPrice * dataManager.intervallMultiplier));
                dataManager.WhatPurchased(3);
                dataManager.UpdateCoins();
                intervallText.text = "Level: " + Mathf.Round((dataManager.intervallMultiplier - 1) / upgradeAdd + 1) + " Multiplier: " + dataManager.intervallMultiplier + "\nCoins: " + intervallPrice * dataManager.intervallMultiplier;
            }
            else
            {
                intervallText.text = "Maxed! Multiplier: " + dataManager.intervallMultiplier;
            }
        }
    }

    public void SelectHealth()
    {
        if (dataManager.healthMultiplier <= 1 + upgradeAdd * maxUpgrade)
        {
            if (dataManager.GetCoins() >= healthPrice * dataManager.healthMultiplier)
            {
                dataManager.AddCoins(-(int)Mathf.Round(healthPrice * dataManager.healthMultiplier));
                dataManager.WhatPurchased(4);
                dataManager.UpdateCoins();
                healthText.text = "Level: " + Mathf.Round((dataManager.healthMultiplier - 1) / upgradeAdd + 1) + " Multiplier: " + dataManager.healthMultiplier + "\nCoins: " + strengthPrice * dataManager.healthMultiplier;
            }
            else
            {
                healthText.text = "Maxed! Multiplier: " + dataManager.healthMultiplier;
            }
        }  
    }

    private void Start()
    {
        dataManager = GameObject.Find("MenuManager").GetComponent<DataManager>();
        maxUpgrade -= 1;

        if (dataManager.rifleAcquired)
            rifleAcquiredText.text = "Purchased";
        else
            rifleAcquiredText.text = riflePrice + " Coins";

        if (dataManager.rifleAcquired)
            sniperAcquiredText.text = "Purchased";
        else
            sniperAcquiredText.text = sniperPrice + " Coins";


        if (!(dataManager.strengthMultiplier < 1 + upgradeAdd * maxUpgrade))
        {
            strengthText.text = "Maxed! Multiplier: " + dataManager.strengthMultiplier;
        }
        else
        {
            strengthText.text = "Level: " + Mathf.Round((dataManager.strengthMultiplier - 1) / upgradeAdd + 1) + " Multiplier: " + dataManager.strengthMultiplier + "\nCoins: " + strengthPrice * dataManager.strengthMultiplier;
        }

        if (!(dataManager.intervallMultiplier < 1 + upgradeAdd * maxUpgrade))
        {
            intervallText.text = "Maxed! Multiplier: " + dataManager.intervallMultiplier;
        }
        else
        {
            intervallText.text = "Level: " + Mathf.Round((dataManager.intervallMultiplier - 1) / upgradeAdd + 1) + " Multiplier: " + dataManager.intervallMultiplier + "\nCoins: " + intervallPrice * dataManager.intervallMultiplier;
        }

        if (!(dataManager.healthMultiplier < 1 + upgradeAdd * maxUpgrade))
        {
            healthText.text = "Maxed! Multiplier: " + dataManager.healthMultiplier;
        }
        else
        {
            healthText.text = "Level: " + Mathf.Round((dataManager.healthMultiplier - 1) / upgradeAdd + 1) + " Multiplier: " + dataManager.healthMultiplier + "\nCoins: " + healthPrice * dataManager.healthMultiplier;
        }
    }

    public void Cheat()
    {
        dataManager.AddCoins(1000);
        dataManager.UpdateCoins();
    }
}
