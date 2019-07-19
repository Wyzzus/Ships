using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingComponent : EntityComponent
{
    // Start is called before the first frame update
    public Transform FloatingObject;

    [Header("Points")]
    public float WaterLevel;
    public float WaterOffset;
    public Vector3 Center;
    public Vector3 Left;
    public Vector3 Right;
    public Vector3 Front;
    public Vector3 Back;

    public float BouncingT = 1f;
    public float PitchingT = 1f;

    public float SidePitchPower = 1f;

    public override void Start()
    {
        base.Start();
        if (!FloatingObject)
            FloatingObject = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (FloatingObject)
            Floating();
    }


    public void Floating()
    {
        Vector3 pos = FloatingObject.position;
        WaterLevel = WaterSystem.instance.GetWaterLine(pos, Vector3.zero);
        Vector3 newPos = new Vector3(pos.x, WaterLevel + WaterOffset, pos.z);
        //objectToFLoat.position = newPos;

        Left = -FloatingObject.right * 5;
        Left.y = WaterSystem.instance.GetWaterLine(FloatingObject.TransformPoint(Left), Vector3.zero) - WaterLevel;
        Debug.DrawRay(pos, Left, Color.blue);
        Right = FloatingObject.right * 5;
        Right.y = WaterSystem.instance.GetWaterLine(FloatingObject.TransformPoint(Right), Vector3.zero) - WaterLevel;
        Debug.DrawRay(pos, Right, Color.green);
        Front = FloatingObject.forward * 6;
        Front.y = WaterSystem.instance.GetWaterLine(FloatingObject.TransformPoint(Front), Vector3.zero) - WaterLevel;
        Debug.DrawRay(pos, Front, Color.red);
        Back = -FloatingObject.forward * 3;
        Back.y = WaterSystem.instance.GetWaterLine(FloatingObject.TransformPoint(Back), Vector3.zero) - WaterLevel;
        Debug.DrawRay(pos, Back, Color.magenta);
        float yRot = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(GetAngle(Front, Back), yRot, -GetAngle(Right, Left)), PitchingT * Time.deltaTime);

        FloatingObject.position = Vector3.Lerp(pos, newPos, BouncingT * Time.deltaTime);
    }

    public float GetAngle(Vector3 positive, Vector3 negative)
    {
        if (positive.y > negative.y)
        {
            return -Mathf.Atan((positive.y - negative.y) / Vector3.Distance(positive, negative)) * Mathf.Rad2Deg * SidePitchPower;
        }
        return Mathf.Atan((negative.y - positive.y) / Vector3.Distance(positive, negative)) * Mathf.Rad2Deg * SidePitchPower;
    }

}