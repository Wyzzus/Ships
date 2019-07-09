using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Контроллер игрока
//Позволяет управлять кораблем через ShipEntity и другие сущности
[RequireComponent(typeof(ShipEntity))]
public class PlayerController : Controller
{

    public ShipEntity CurrentShipEntity;
    public Vector3 Direction;
    
    public override void Start()
    {
        base.Start();
        CurrentShipEntity = GetComponent<ShipEntity>();
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }
    
    public override void Update()
    {
        base.Update();
        Movement();
        Shooting();
    }

    public void Movement()
    {
        //TODO: Переделать систему ввода
        float forwardPower = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1) * 5;
        float sidePower = Input.GetAxis("Horizontal");
        #region MovePoint Calculation
        Direction = transform.forward + (transform.forward * Mathf.Abs(sidePower) + transform.right * sidePower);
        Direction = transform.position + transform.forward + Direction * forwardPower;
        Debug.DrawRay(transform.position, Direction - transform.position, Color.red);
        Vector3 MovePoint = Vector3.Lerp(transform.position, Direction, 0.5f);
        #endregion
        //if (Direction.magnitude > 2f)
        //if(Mathf.Abs(forwardPower) > 0 || Mathf.Abs(sidePower) > 0)
        {
            CurrentShipEntity.MoveTo(MovePoint);
        }
    }

    public void Shooting()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            CurrentShipEntity.FireLeft();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            CurrentShipEntity.FireRight();
        }
    }
}
