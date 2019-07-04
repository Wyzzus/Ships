using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Сущность, способная передвигаться
//Основа для сущности корабля и объектов, которые передвигаются по навмешу
[RequireComponent(typeof(MotorComponent))]
public class MovableEntity : Entity
{
    // Start is called before the first frame update

    public MotorComponent Motor;

    public override void Start()
    {
        base.Start();
        Motor = (MotorComponent)GetComponentOfType<MotorComponent>();
    }

    public void MoveTo(Vector3 point)
    {
        Motor.MoveTo(point);
    }

}
