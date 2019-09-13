using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAIController : AIController
{
    [Header ("Entities")]
    public ShipEntity CurrentShipEntity;
    public ShipAIEntity CurrentShipAI;

    public override void Start()
    {
        base.Start();
        CurrentShipEntity = GetComponent<ShipEntity>();
        CurrentShipAI = GetComponent<ShipAIEntity>();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Behaviuor();
    }

    public override void Behaviuor()
    {
        base.Behaviuor();

        if (CurrentShipAI.Vision.HasTarget)
        {
            CurrentShipEntity.Motor.MoveTo(CurrentShipAI.Vision.CurrentTarget.transform.position);
        }
    }
}
