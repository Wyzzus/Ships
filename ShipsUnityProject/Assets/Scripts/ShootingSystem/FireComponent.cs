using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireComponent : EntityComponent
{
    public List<Transform> FirePoints;
    [Header ("CoolDown")]
    [SerializeField]
    public float CoolDown;
    public float CoolDownCounter;
    public bool CanShoot;

    [Header("Projectile")]
    public GameObject Projectile;
    public float TimeToLive;

    public override void Start()
    {
        base.Start();
        GetFirePoints();
    }

    public virtual void GetFirePoints()
    {
        FirePoints = new List<Transform>();
        Transform[] children = GetComponentsInChildren<Transform>();
        foreach (Transform child in children)
        {
            if (child.CompareTag("FirePoint"))
            {
                FirePoints.Add(child);
            }
        }
    }

    public void FireAll()
    {
        if(CanShoot)
            Fire(FirePoints);
    }

    public void Fire(List<Transform> firePoints)
    {
        foreach (Transform firePoint in firePoints)
        {
            GameObject projectile = Instantiate<GameObject>(Projectile, firePoint.position, firePoint.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 1000f);
            Destroy(projectile, TimeToLive);
        }
        CoolDownCounter = CoolDown;
    }

    public void Update()
    {
        if (CoolDownCounter > 0)
        {
            CoolDownCounter -= Time.deltaTime;
            CanShoot = false;
        }
        else
        {
            CanShoot = true;
        }
    }
    
}
