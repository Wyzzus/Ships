using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Сущность корабля
//Управляет компонентами корабля
public class ShipEntity : MovableEntity
{
    public override void Start()
    {
        base.Start();
    }
    
    void Update()
    {
        
    }

    public void MoveTo(Vector3 point)
    {
        Motor.MoveTo(point);
    }
}
