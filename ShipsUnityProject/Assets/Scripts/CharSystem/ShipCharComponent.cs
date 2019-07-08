using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCharComponent : CharacteristicsComponent
{
    [Header("Movement")]
    public float BaseMoveSpeed = 1f;
    public float CurrentMoveSpeed;
    public float BaseTurnSpeed = 1f;
    public float CurrentTurnSpeed;
    public bool CanMove = true;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public void SetMoveAbility(bool value)
    {
        CanMove = value;
    }
    
    public void SetupMovement()
    {
        CurrentMoveSpeed = BaseMoveSpeed * GetLevelKoeff();
        CurrentTurnSpeed = BaseTurnSpeed * GetLevelKoeff();
    }
}
