using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class WaterGun : MonoBehaviour
{
    [SerializeField] private float MaxDistance = 2.5f;
    [SerializeField] private float MinTriggerValue = 0.1f;

    [SerializeField] private LayerMask Mask;
    [SerializeField] private Transform RayAttachPoint;

    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private AudioSource _audioEffectSource;

    [SerializeField] private Texture2D _brush;

    public InputActionProperty? PinchAnimateAction { get; private set; } = null;

    private float triggerValue = 0;

    public void OnPinch(InputActionProperty pinchAction) => PinchAnimateAction = pinchAction;

    public void OffPinch() => PinchAnimateAction = null;

    void Update()
    {
        if (PinchAnimateAction is not null)
        {
            InputActionProperty newPinchAnimateAction = (InputActionProperty)PinchAnimateAction;
            triggerValue = newPinchAnimateAction.action.ReadValue<float>();
            Debug.Log("I'm Enable!! TriggerValue = " + triggerValue);
            if (Input.GetButton("Fire1") || triggerValue > MinTriggerValue)
            {
                Debug.Log("Fire");
                Shoot();
            }
        }
        else
        {
            triggerValue = 0;
        }
    }

    void Shoot()
    {
        _audioEffectSource?.PlayOneShot(_audioEffectSource.clip);

        _shotEffect.Play();

        CollideRay();
    }

    void CollideRay()
    {
        Ray ray = new Ray(RayAttachPoint.position, RayAttachPoint.forward);
        Debug.DrawRay(ray.origin, ray.direction * triggerValue * MaxDistance, Color.yellow);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, triggerValue * MaxDistance, Mask))
        {
            Debug.Log("Hit");
            CleanableObject cleanable = hit.collider.gameObject.GetComponent<CleanableObject>();

            if (cleanable)
            {
                cleanable.Change(hit, _brush);
            }
        }
    }
}
