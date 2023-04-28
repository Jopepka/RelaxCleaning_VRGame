using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandInput : MonoBehaviour
{
    public InputActionProperty pinchAnimateAction;
    public InputActionProperty grapAnimateAction;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = pinchAnimateAction.action.ReadValue<float>();

        float gripValue = grapAnimateAction.action.ReadValue<float>();
    }
}
