using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Сущность корабля
//Управляет компонентами корабля
public class ShipEntity : MovableEntity
{
    public List<FireComponent> LeftCannons;
    public List<FireComponent> RightCannons;

    public override void Start()
    {
        base.Start();
        GetSides();
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
}
