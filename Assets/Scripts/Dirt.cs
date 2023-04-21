using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.Overlays;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    private double HP = 100;
    public bool IsDeade { get; private set; } = false;

    void Start()
    {
        gameObject.layer = 3;
        Debug.Log("Get mask real name2 '" + LayerMask.LayerToName(gameObject.layer) + "'");
    }

    void Update()
    {
        if (HP <= 0)
        {
            IsDeade = true;
            Debug.Log("Object " + gameObject.name + " is dead");
            Destroy(gameObject);
        }
    }

    public void DealDamage(double distance)
    {
        if (!IsDeade)
        {
            double damage = calculateDamage(distance, 0.1);

            HP -= damage;

            Debug.Log("Object '" + gameObject.name + "' get " + damage.ToString() + " damage. HP left " + HP.ToString());
        }
    }

    double calculateDamage(double distance, double UnitDamage)
    {
        return UnitDamage / distance;
    }
}
