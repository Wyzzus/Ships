using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAIVisionComponent : AIVisionComponent
{
    [Header ("Vision")]
    public SphereCollider VisionZone;
    public float VisionRadius;

    [Header ("Target")]
    public bool HasTarget = false;
    public ShipEntity CurrentTarget;
    public float ChaseDistance = 1000f; //Дистанция потери игрока из виду, стандартно - 1км

    // Start is called before the first frame update
    public override void Start()
    {
        GenerateVisionZone();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (CurrentTarget != null)
        {
            if (Vector3.Distance(transform.position, CurrentTarget.transform.position) > ChaseDistance)
            {
                HasTarget = false;
                CurrentTarget = null;
            }
        }
    }

    public override void GenerateVisionZone()
    {
        VisionZone = gameObject.AddComponent<SphereCollider>();
        VisionZone.radius = VisionRadius;
        VisionZone.isTrigger = true;
    }

    public void OnTriggerStay(Collider obj)
    {
        if (!HasTarget)
        {
            ShipEntity ship = obj.GetComponentInParent<ShipEntity>();
            if (ship != null)
            {
                CurrentTarget = ship;
                HasTarget = true;
            }
        }
    }

    
}
