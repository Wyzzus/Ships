﻿using System.Collections;
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

    public float MoveSpeed = 10;
    public float TurnSpeed = 1;
    public float Acceleration = 1;
    public float CurrrentMoveSpeed = 0;
    public float CurrentTurnSpeed = 0;

    public override void Start()
    {
        base.Start();
        Agent = GetComponent<NavMeshAgent>();
        Path = new NavMeshPath();
    }

    public void MoveTo(Vector3 point)
    {
        /*if (Agent && Agent.enabled)
        {
            //Условие прописано на всякий случай
            //Если вдруго пропадет галочка Auto Braking у NavMeshAgent
            if (Vector3.Distance(point, transform.position) > Agent.stoppingDistance)
            {
                Agent.SetDestination(point);
            }
            else
            {
                Agent.ResetPath();
            }
        }*/

        if (Distance2D(point) > 1f)
        {
            CurrrentMoveSpeed = Mathf.Lerp(CurrrentMoveSpeed, MoveSpeed, Time.deltaTime * Acceleration);
            CurrentTurnSpeed = Mathf.Lerp(CurrentTurnSpeed, TurnSpeed, Time.deltaTime * Acceleration);
        }
        else
        {
            CurrrentMoveSpeed = Mathf.Lerp(CurrrentMoveSpeed, 0, Time.deltaTime * Acceleration);
            CurrentTurnSpeed = Mathf.Lerp(CurrentTurnSpeed, 0, Time.deltaTime * Acceleration);
        }

        if (CurrrentMoveSpeed > 0)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, Time.deltaTime * CurrrentMoveSpeed);
            //transform.position += transform.forward * Time.deltaTime * MoveSpeed;
        }

        if (CurrentTurnSpeed > 0)
        {
            Vector3 direction = (GetPointOfPath(point) - transform.position).normalized;
            direction = new Vector3(direction.x, 0, direction.z);
            Quaternion lookRot = Quaternion.LookRotation(direction);
            float yTurn = lookRot.eulerAngles.y;
            if (direction.magnitude > 0)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, yTurn, transform.rotation.eulerAngles.z), CurrentTurnSpeed * Time.deltaTime);

        }

    }

    public Vector3 GetPointOfPath(Vector3 point)
    {
        NavMesh.CalculatePath(transform.position, point, PathMask, Path);
        if ((Path != null && Path.corners.Length > 0) && Vector3.Distance(point, transform.position) > 10f)
        {
            return Path.corners[0];
        }
        else
        {
            return point + transform.forward;
        }
    }

    public float Distance2D(Vector3 point)
    {
        Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
        return Vector3.Distance(pos, new Vector3(point.x, 0, point.z));
    }
}
