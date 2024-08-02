using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    public enum playerType
    {
        player,
        ai
    }

    public playerType PlayerType;


    private InputManager inputManager;
    private PlayerMovement playerMovement;
    private DataManager dataManager;

    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private ParticleSystem gunParticleSystem;

    [Space]
    [Header("Settings")]
    [SerializeField]
    [Range(1, 100)]
    private float bulletForce;

    [Range(1, 100)]
    public float normalDamage, cricicalHitChance;

    [SerializeField]
    [Range(0, 0.5f)]
    private float damageVariation;

    [SerializeField]
    [Range(0, 10)]
    private float startTimeBtwShots, spray;

    [SerializeField]
    [Range(0, 200)]
    private float startSprayMultiplierOnMoving;



    [Space]
    [Header("Ammunation")]
    [SerializeField]
    [Range(0, 100)]
    private int startMagazine;

    [SerializeField]
    [Range(0, 10)]
    private float reloadingTime;

    private float damage;
    private int magazine;
    private bool isReloading;
    private float sprayMultiplierOnMoving;

    private float strengthMultiplier, intervallMultiplier;

    //AI
    private FoeAI foeAI;
    private float timeBtwShots;

    private void Start()
    {
        switch (PlayerType)
        {
            case playerType.player:
                inputManager = gameObject.GetComponent<InputManager>();
                playerMovement = gameObject.GetComponent<PlayerMovement>();
                dataManager = GameObject.Find("GameManager").GetComponent<DataManager>();

                strengthMultiplier = dataManager.GetMultiplierStrength();
                intervallMultiplier = dataManager.GetMultiplierIntervall();
                print(strengthMultiplier);
                print(intervallMultiplier);
                
                magazine = startMagazine;
                sprayMultiplierOnMoving = startSprayMultiplierOnMoving;

                playerMovement = gameObject.GetComponent<PlayerMovement>();
                playerMovement.ammunationText.text = startMagazine.ToString();
                startTimeBtwShots = startTimeBtwShots * 1 / intervallMultiplier;
                break;
            case playerType.ai:
                foeAI = gameObject.GetComponent<FoeAI>();
                timeBtwShots = startTimeBtwShots;
                break;
        }
    }

    private void Update()
    {
        switch (PlayerType)
        {
            case playerType.player:
                PlayerShoot();
                break;
            case playerType.ai:
                AIShoot();
                break;
        } 
    }

    private void PlayerShoot()
    {
        if (inputManager.LeftClick() && timeBtwShots <= 0)
        {
            damageDetermination();
            Shoot();
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (playerMovement.isMoving)
        {
            sprayMultiplierOnMoving = startSprayMultiplierOnMoving;
            
        }
        else
        {
            sprayMultiplierOnMoving = 1;
        }

        if (inputManager.R() && !isReloading && magazine != startMagazine){
            StartCoroutine(WaitToReload());
        }
    }

    private void AIShoot()
    {
        if (timeBtwShots <= 0 && foeAI.readyToShoot)
        {
            damageDetermination();
            Shoot();
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    private void damageDetermination()
    {
        damage = UnityEngine.Random.Range(normalDamage * strengthMultiplier * (1 - damageVariation), normalDamage * strengthMultiplier * (1 + damageVariation));

        int random = UnityEngine.Random.Range(0, 100);

        if (random <= cricicalHitChance)
        {
            damage *= 10;
        }
    }
        
    private void Shoot()
    {
        if (!isReloading)
        {
            Quaternion backupRotation = firePoint.rotation;
            Vector3 firePointRotationAsVector = firePoint.rotation.eulerAngles;
            firePointRotationAsVector.y = UnityEngine.Random.Range(firePointRotationAsVector.y - spray * sprayMultiplierOnMoving, firePointRotationAsVector.y + spray * sprayMultiplierOnMoving);
            firePoint.rotation = Quaternion.Euler(firePointRotationAsVector);

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.bulletDamage = damage;

            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);
            firePoint.rotation = backupRotation;
            gunParticleSystem.Play();

            Reloading();
        }
    }

    private void Reloading()
    {
        magazine -= 1;
        if (PlayerType == playerType.player)
        {
            playerMovement.ammunationText.text = magazine.ToString();
        }

        if (magazine <= 0 && !isReloading)
        {
            StartCoroutine(WaitToReload());
        }
    }

    IEnumerator WaitToReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadingTime);
        magazine = startMagazine;
        isReloading = false;
        if (PlayerType == playerType.player)
        {
            playerMovement.ammunationText.text = magazine.ToString();
        }
    }
}
