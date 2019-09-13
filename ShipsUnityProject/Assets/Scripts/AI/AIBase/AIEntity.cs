using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntity : Entity
{

    public override void Start()
    {
        Components = new List<EntityComponent>();
        GetAIComponents();
    }

    public virtual void GetAIComponents()
    {

    }
}
