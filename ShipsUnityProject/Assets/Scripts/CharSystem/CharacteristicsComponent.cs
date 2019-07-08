using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteristicsComponent : EntityComponent
{
    
    [Header ("Info")]
    public string Name;
    public int FractionID;
    public int Level = 0;

    [Header ("HP")]
    public float BaseHP = 100f;
    public float CurrentHP;
    public bool Alive = true;


    public override void Start()
    {
        base.Start();
        SetupChars();
    }
    public virtual void Update() 
    {
        HPCheck();
    }

    public void SetupChars()
    {
        CurrentHP = BaseHP * GetLevelKoeff();
    }

    public virtual float GetLevelKoeff()
    {
        return 1 + (float)Level / 10f;
    }

    public void HPCheck()
    {
        if(CurrentHP > 0) 
            Alive = true;
        else 
            Alive = false;
    }
    
    public int GetDamageLevel()
    {
        
        return 0;
    }

    public virtual void TakeDamage(float value)
    {
        CurrentHP -= value;
    }
}
