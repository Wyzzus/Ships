using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipAIVisionComponent))]
public class ShipAIEntity : AIEntity
{
    public ShipAIVisionComponent Vision;


    // Start is called before the first frame update
    public override void Start()
    {
        Vision = GetComponentInChildren<ShipAIVisionComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
