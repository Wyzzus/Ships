using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Компонент управления передвижением обхектов, находящихся на NavMesh'e
[RequireComponent(typeof(NavMeshAgent))]
public class MotorComponent : EntityComponent
{
    NavMeshAgent Agent;
    NavMeshPath Path;
    public LayerMask PathMask = -1;

    public float MoveSpeed = 10f;
    public float TurnSpeed = 1f;
    public float MoveAcceleration = 1f;
    public float TurnAcceleration = 1f;
    public float CurrrentMoveSpeed = 0;
    public float CurrentTurnSpeed = 0;
    public float StopDistance = 2f;

    public override void Start()
    {
        base.Start();
        Agent = GetComponent<NavMeshAgent>();
        Path = new NavMeshPath();
    }

    public void MoveTo(Vector3 point)
    {
        if (Distance2D(point) > StopDistance)
        {
            CurrrentMoveSpeed = Mathf.Lerp(CurrrentMoveSpeed, MoveSpeed, Time.deltaTime * MoveAcceleration / 10f);
            CurrentTurnSpeed = Mathf.Lerp(CurrentTurnSpeed, TurnSpeed, Time.deltaTime * TurnAcceleration / 10f);
        }
        else
        {
            CurrrentMoveSpeed = Mathf.Lerp(CurrrentMoveSpeed, 0, Time.deltaTime * MoveAcceleration / 10f);
            CurrentTurnSpeed = Mathf.Lerp(CurrentTurnSpeed, 0, Time.deltaTime * TurnAcceleration / 10f);
        }

        if (CurrrentMoveSpeed > 0)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, Time.deltaTime * CurrrentMoveSpeed);
        }

        if (CurrentTurnSpeed > 0)
        {
            Vector3 direction = (GetPointOfPath(point) - transform.position);
            direction = new Vector3(direction.x, 0, direction.z);

            if (direction.magnitude > Mathf.Epsilon)
            {
                Quaternion lookRot = Quaternion.LookRotation(direction);
                float yTurn = lookRot.eulerAngles.y;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, yTurn, transform.rotation.eulerAngles.z), CurrentTurnSpeed * Time.deltaTime / 10f);
            }

        }

    }

    public Vector3 GetPointOfPath(Vector3 point)
    {
        NavMesh.CalculatePath(transform.position, point, PathMask, Path);
        if ((Path != null && Path.corners.Length > 0) && Vector3.Distance(point, transform.position) > 10f)
        {
            return Path.corners[1];
        }
        else
        {
            return point;
        }
    }

    public float Distance2D(Vector3 point)
    {
        Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
        return Vector3.Distance(pos, new Vector3(point.x, 0, point.z));
    }
}
