using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Компонент управления передвижением обхектов, находящихся на NavMesh'e
[RequireComponent(typeof(NavMeshAgent))]
public class MotorComponent : EntityComponent
{
    NavMeshAgent Agent;
    
    public override void Start()
    {
        base.Start();
        Agent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 point)
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
    }
}
