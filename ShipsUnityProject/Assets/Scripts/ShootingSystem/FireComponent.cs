using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireComponent : EntityComponent
{
    public List<Transform> FirePoints;
    public float ShotPower = 1000f;
    [Header ("CoolDown")]
    [SerializeField]
    public float CoolDown;
    public float CoolDownCounter;
    public bool CanShoot;

    [Header("Projectile")]
    public GameObject Projectile;
    public float TimeToLive;
    public float ShootDelay = 1f;
    public Vector3 Deviation;

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
            StartCoroutine(FireCoroutine(FirePoints));
    }

    public IEnumerator FireCoroutine(List<Transform> firePoints)
    {
        WaitForSeconds delay = new WaitForSeconds(Random.Range(0, ShootDelay));
        CoolDownCounter = CoolDown;
        foreach (Transform firePoint in firePoints)
        {
            float x = firePoint.rotation.eulerAngles.x + Random.Range(-Deviation.x, Deviation.x);
            float y = firePoint.rotation.eulerAngles.y + Random.Range(-Deviation.y, Deviation.y);
            float z = 0;
            Quaternion startRotation = Quaternion.Euler(x, y, z);
            GameObject projectile = Instantiate<GameObject>(Projectile, firePoint.position, startRotation);
            projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * ShotPower);
            Destroy(projectile, TimeToLive);
            yield return delay;
        }
        yield return null;
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
