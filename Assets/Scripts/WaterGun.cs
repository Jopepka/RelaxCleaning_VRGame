using UnityEngine;

public class WaterGun : MonoBehaviour
{
    public float Distance;
    public LayerMask Mask;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * Distance, Color.yellow);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Distance, Mask))
        {
            hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            Debug.Log("Hit sath");
            Dirt dirt = hit.collider.gameObject.GetComponent<Dirt>();

            if (dirt)
            {
                dirt.DealDamage(hit.distance);
            }
        }
    }
}
