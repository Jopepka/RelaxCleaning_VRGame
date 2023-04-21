using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.Overlays;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    private float HP = 100;
    private Vector3 StartScale;
    public bool IsDeade { get; private set; } = false;


    void Start()
    {
        gameObject.layer = 3;
        Debug.Log("Get mask real name2 '" + LayerMask.LayerToName(gameObject.layer) + "'");

        StartScale = transform.localScale;
    }

    void Update()
    {
        if (HP <= 10)
        {
            IsDeade = true;
            Debug.Log("Object " + gameObject.name + " is dead");
            Destroy(gameObject);
        }
    }

    public void DealDamage(float distance)
    {
        if (!IsDeade)
        {
            float damage = calculateDamage(distance, 0.1f);

            HP -= damage;

            transform.localScale = new Vector3(StartScale.x * (HP) / 100, StartScale.y * (HP) / 100, StartScale.z * (HP) / 100);
            Debug.Log("Object '" + gameObject.name + "' get " + damage.ToString() + " damage. HP left " + HP.ToString());
        }
    }

    float calculateDamage(float distance, float UnitDamage)
    {
        return UnitDamage / distance;
    }
}
