using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipFireComponent : FireComponent
{
    public List<Transform> LeftFirePoints;
    public List<Transform> RightFirePoints;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        GetSideFirePoints();
    }

    public void GetSideFirePoints()
    {
        LeftFirePoints = new List<Transform>();
        RightFirePoints = new List<Transform>();
        foreach (Transform firePoint in FirePoints)
        {
            //Debug.Log(Vector3.SignedAngle(transform.forward, firePoint.forward, Vector3.one));
            if (Vector3.SignedAngle(transform.forward, firePoint.forward, Vector3.one) > 0)
            {
                RightFirePoints.Add(firePoint);
            }
            else
            {
                LeftFirePoints.Add(firePoint);
            }
        }
    }

    public void FireLeft()
    {
        Fire(LeftFirePoints);
    }

    public void FireRight()
    {
        Fire(RightFirePoints);
    }
}
