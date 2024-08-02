using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject normal, foe;

    [SerializeField]
    [Range(1, 15)]
    private float destroyTime;

    //[HideInInspector]
    public float bulletDamage;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Foe" || other.tag == "Player")
        {
            GameObject effect = Instantiate(foe, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = effect.GetComponent<ParticleSystem>();
            particleSystem.Play();

            //DamagePopup.Create(GetComponentInChildren<Transform>(), gameObject.transform.position, bulletDamage);
            Destroy(effect, 3f);
        }
        else
        {
            GameObject effect = Instantiate(normal, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = effect.GetComponent<ParticleSystem>();
            particleSystem.Play();
            Destroy(effect, 3f);
        }
        Destroy(gameObject);
    }

    private void Update()
    {
        Destroy(gameObject, destroyTime);
    }
}
