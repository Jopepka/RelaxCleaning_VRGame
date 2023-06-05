using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class TotalDirtCounter : MonoBehaviour, IDirtCounter
{
    private void Update()
    {
        Debug.Log($"Total : {GetDirtAmout()} %");
    }

    public float GetDirtAmout()
    {
        var totalDirtAmount = 0f;
        var count = 0;

        foreach (Transform child in transform)
        {
            var cleanable = child.gameObject.GetComponentInChildren<CleanableObject>();
            totalDirtAmount += cleanable.GetDirtAmout();
            count++;
        }
        return totalDirtAmount / count;
    }
}
