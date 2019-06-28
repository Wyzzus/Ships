using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Контроллер игрока
//Позволяет управлять кораблем через ShipEntity и другие сущности
[RequireComponent(typeof(ShipEntity))]
public class PlayerController : Controller
{

    public ShipEntity CurrentShipEntity;
    public Transform Pointer;
    public Vector3 Direction;
    
    public override void Start()
    {
        base.Start();
        CurrentShipEntity = GetComponent<ShipEntity>();
    }
    
    public override void Update()
    {
        base.Update();
        Movement();
    }

    public void Movement()
    {
        Direction.x = Input.GetAxis("Horizontal") * 5;
        Direction.z = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1) * 10;
        Direction.Normalize();

        Pointer.localPosition = Vector3.Lerp(Pointer.localPosition, (Direction + Vector3.forward) * 5, 0.5f);

        if (Direction.magnitude > 0.2f)
        {
            CurrentShipEntity.MoveTo(Pointer.position);
        }
    }
}
