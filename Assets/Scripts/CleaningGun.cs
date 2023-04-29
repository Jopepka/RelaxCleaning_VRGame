using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningGun : MonoBehaviour
{
    public float Distance;
    public LayerMask Mask;

    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private AudioSource _audioEffectSource;
    [SerializeField] private AudioClip _audioEffect;
    [SerializeField] private Texture2D _brush;
    [SerializeField] private Camera _camera;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Fire1");
            Shoot();
        }
    }

    void Shoot()
    {
        //_audioEffectSource.PlayOneShot(_audioEffect);
        _shotEffect.Play();

        CollideRay();
    }

    void CollideRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * Distance, Color.yellow);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Distance, Mask))
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
