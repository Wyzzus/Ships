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
    public Transform tracker;
    
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
        float forwardPower = Mathf.Clamp(Input.GetAxis("Vertical"), 0, 1) * 10;
        float sidePower = Input.GetAxis("Horizontal");
        #region Direction Calculation
        Direction = transform.forward + (transform.right * sidePower);
        Direction = transform.position + transform.forward + Direction * forwardPower * 2;
        Debug.DrawRay(transform.position, Direction - transform.position, Color.red);
        #endregion
        //if (Direction.magnitude > 2f)
        //if(Mathf.Abs(forwardPower) > 0 || Mathf.Abs(sidePower) > 0)
        {
            CurrentShipEntity.MoveTo(Direction);
        }
    }

    public void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CurrentShipEntity.LeftAim(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CurrentShipEntity.RightAim(true);
        }

        if (Input.GetKeyUp(KeyCode.Q))
        {
            CurrentShipEntity.FireLeft();
            CurrentShipEntity.LeftAim(false);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            CurrentShipEntity.FireRight();
            CurrentShipEntity.RightAim(false);
        }
    }
}
