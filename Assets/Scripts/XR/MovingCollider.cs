using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCollider : MonoBehaviour
{
    [SerializeField] private GameObject moveObj;
    [SerializeField] private Behaviour followTo;
    
    void Update()
    {
        var x = followTo.transform.position.x;
        var z = followTo.transform.position.z;

        moveObj.transform.position = new Vector3(x, moveObj.transform.position.y, z);
    }
}

