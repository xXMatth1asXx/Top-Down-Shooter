using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField]
    [Range(1, 50)]
    private float bulletForce;

    [SerializeField]
    [Range(0, 0.5f)]
    private float damageVariation;

    [Range(1, 100)]
    public float normalDamage, cricicalHitChance;

    private float damage;

    [SerializeField]
    [Range(0, 10)]
    private float startTimeBtwShots;
}
