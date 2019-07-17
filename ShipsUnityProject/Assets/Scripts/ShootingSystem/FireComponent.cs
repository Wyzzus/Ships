using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

    [Header("Shooting Zone")]
    public bool NeedCalculateZone = true;
    public Vector3 ZoneCenter;
    public Vector3 ZoneScale;

    public override void Start()
    {
        base.Start();
        GetFirePoints();
        //CalculateShootingZone();
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
    #region Legacy
    //Старый расчет зоны выстрела
    //Оставил потому, что он может еще понадобиться. Но не факт
    /*public void CalculateShootingZone()
    {
        Vector3 FirstLocal = transform.InverseTransformPoint(FirePoints[0].position);
        float MaxZ = FirstLocal.z;
        float MinZ = FirstLocal.z;
        float MaxY = FirstLocal.y;
        float MinY = FirstLocal.y;
        foreach (Transform fp in FirePoints)
        {
            Vector3 LocalFP = transform.InverseTransformPoint(fp.position);
            if (LocalFP.z >= MaxZ)
            {
                MaxZ = LocalFP.z;
            }

            if (LocalFP.z <= MinZ)
            {
                MinZ = LocalFP.z;
            }

            if (LocalFP.y >= MaxY)
            {
                MaxY = LocalFP.y;
            }

            if (LocalFP.y <= MinY)
            {
                MinY = LocalFP.y;
            }
        }
        
        ZoneCenter = new Vector3((MaxZ + MinZ) / 2f, (MaxY + MinY) / 2f);
        ZoneScale = new Vector3(MaxZ - MinZ, MaxY - MinY);
    }*/
    #endregion
}

/*

#region WidthCalculation
            if (transform.InverseTransformPoint(fp.position).z > 0)
            {
                float curFrontDist = Vector3.Distance(FrontPoint, transform.InverseTransformPoint(fp.position));
                if (curFrontDist > FrontMaxDist)
                {
                    FrontPoint = fp.position;
                    FrontMaxDist = curFrontDist;
                    one = fp;
                }
            }
            else
            {
                float curBackDist = Vector3.Distance(BackPoint, transform.InverseTransformPoint(fp.position));
                if (curBackDist > BackMaxDist)
                {
                    BackPoint = fp.position;
                    BackMaxDist = curBackDist;
                    two = fp;
                }
            }
            #endregion
            #region HeightCalculation
            if (transform.InverseTransformPoint(fp.position).y > 0)
            {
                float curUpDist = Vector3.Distance(UpPoint, transform.InverseTransformPoint(fp.position));
                if (curUpDist > UpMaxDist)
                {
                    UpPoint = fp.position;
                    UpMaxDist = curUpDist;
                    three = fp;
                }
            }
            else
            {
                float curDownDist = Vector3.Distance(DownPoint, transform.InverseTransformPoint(fp.position));
                if (curDownDist > DownMaxDist)
                {
                    DownPoint = fp.position;
                    DownMaxDist = curDownDist;
                    four = fp;
                }
            }
            #endregion
 
*/
