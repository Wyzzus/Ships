using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//Сущность корабля
//Управляет компонентами корабля
public class ShipEntity : MovableEntity
{
    [Header("Left Side")]
    public List<FireComponent> LeftCannons;
    public Vector3 LeftZoneScale;
    public Vector3 LeftZoneCenter;

    [Header("Right Side")]
    public List<FireComponent> RightCannons;
    public Vector3 RightZoneScale;
    public Vector3 RightZoneCenter;

    public GraphicsComponent GFXComponent;

    


    public override void Start()
    {
        base.Start();
        GetSides();
        GFXComponent = GetComponentInChildren<GraphicsComponent>();
        CalculateShootingZone(RightCannons,out RightZoneCenter, out RightZoneScale);
        CalculateShootingZone(LeftCannons, out LeftZoneCenter, out LeftZoneScale);
    }

    public void GetSides()
    {
        FireComponent[] Sides = GetComponentsInChildren<FireComponent>();
        foreach (FireComponent side in Sides)
        {
            if (side.transform.localPosition.x > 0)
            {
                RightCannons.Add(side);
            }
            else
            {
                LeftCannons.Add(side);
            }
        }
    }

    public void Fire()
    {
        FireLeft();
        FireRight();
    }

    public void FireLeft()
    {
        foreach (FireComponent fc in LeftCannons)
        {
            fc.FireAll();
        }
    }

    public void FireRight()
    {
        foreach (FireComponent fc in RightCannons)
        {
            fc.FireAll();
        }
    }

    public void LeftAim(bool value)
    {
        GFXComponent.ShowAimLeft(value, LeftZoneCenter, LeftZoneScale);
    }

    public void RightAim(bool value)
    {
        GFXComponent.ShowAimRight(value, RightZoneCenter, RightZoneScale);

    }

    public void CalculateShootingZone(List<FireComponent> Cannons, out Vector3 Center, out Vector3 Scale)
    {
        List<Transform> FirePoints = new List<Transform>();
        foreach (FireComponent fc in Cannons)
        {
            FirePoints = FirePoints.Concat<Transform>(fc.FirePoints).ToList<Transform>();
        }

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

        Center = new Vector3(-(MaxZ + MinZ) / 2f, (MaxY + MinY) / 2f);
        Scale = new Vector3(MaxZ - MinZ, MaxY - MinY, 100);
    }
}
