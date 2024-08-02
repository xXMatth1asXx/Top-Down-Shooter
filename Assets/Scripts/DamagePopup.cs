using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{

    public static DamagePopup Create(Transform popup, Vector3 position, float bulletDamage)
    {
        Transform damagePopupTransform = Instantiate(popup, position, Quaternion.identity);

        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        bulletDamage = Mathf.Round(bulletDamage);
        damagePopup.Setup( (int)bulletDamage);
        damagePopupTransform.gameObject.SetActive(true);
        return damagePopup;
    }

    private TextMeshPro textMesh;

    private void Start()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
    }
}
