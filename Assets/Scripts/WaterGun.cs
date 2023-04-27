using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public float Distance;
    public LayerMask Mask;

    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private AudioSource _audioEffectSource;
    [SerializeField] private AudioClip _audioEffect;

    [SerializeField] private Camera _cam;

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Debug.Log("Fire");
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
            hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.yellow; //Потом можно удалить
            Debug.Log("Hit sath");
            Dirt dirt = hit.collider.gameObject.GetComponent<Dirt>();

            if (dirt)
            {
                dirt.DealDamage(hit.distance);
            }
        }
    }
}
