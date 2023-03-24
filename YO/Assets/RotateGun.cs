using UnityEngine;

public class RotateGun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float firerate = 15f;
    public float impactForce = 30f;
    public float muzzleForce = 1f;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

        //I am The Legendary Satak
        void Update()
        {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire) 
          {
            nextTimeToFire = Time.time + 1f / firerate;
            Shoot();
          }
        }
    void Shoot ()
    {
        muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) 
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamge(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }

    }

}