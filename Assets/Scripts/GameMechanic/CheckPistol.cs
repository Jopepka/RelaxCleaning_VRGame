using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CheckPistol : MonoBehaviour
{
    public InputActionProperty pinchAnimateAction;
    /*    if (other.gameObject.CompareTag("washpistol"))
        {
            Debug.Log("Taked pistol");
            other.gameObject.GetComponent<WaterGun>().ChangeState(true);
        }*/

    private void OnTriggerEnter(Collider other)
    {
        WaterGun waterGun = other.gameObject.GetComponent<WaterGun>();
        if (waterGun != null && waterGun.PinchAnimateAction is null)
        {
            Debug.Log("Taked pistol");
            waterGun.OnPinch(pinchAnimateAction);
        }
    }           

    private void OnTriggerExit(Collider other)
    {
        WaterGun waterGun = other.gameObject.GetComponent<WaterGun>();
        if (waterGun != null && pinchAnimateAction == waterGun.PinchAnimateAction)        {
            Debug.Log("Droped pistol");
            waterGun.OffPinch();
        }
    }
}
