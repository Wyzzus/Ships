using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralNoiseProject
{
    public class FloatingComponent : EntityComponent
    {
        // Start is called before the first frame update
        public Transform objectToFLoat;

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
        }

        // Update is called once per frame
        void Update()
        {
            if(objectToFLoat)
                Floating();
        }
        

        public void Floating()
        {
            Vector3 pos = objectToFLoat.position;
            WaterLevel = WaterSystem.instance.GetWaterLine(pos, Vector3.zero);
            Vector3 newPos = new Vector3(pos.x, WaterLevel + WaterOffset, pos.z);
            //objectToFLoat.position = newPos;

            Left = -objectToFLoat.right * 5;
            Left.y = WaterSystem.instance.GetWaterLine(objectToFLoat.TransformPoint(Left), Vector3.zero) - WaterLevel;
            //Debug.DrawRay(pos, Left, Color.blue);
            Right = objectToFLoat.right * 5;
            Right.y = WaterSystem.instance.GetWaterLine(objectToFLoat.TransformPoint(Right), Vector3.zero) - WaterLevel;
            //Debug.DrawRay(pos, Right, Color.green);
            Front = objectToFLoat.forward * 3;
            Front.y = WaterSystem.instance.GetWaterLine(objectToFLoat.TransformPoint(Front), Vector3.zero) - WaterLevel;
            //Debug.DrawRay(pos, Front, Color.red);
            Back = -objectToFLoat.forward * 3;
            Back.y = WaterSystem.instance.GetWaterLine(objectToFLoat.TransformPoint(Back), Vector3.zero) - WaterLevel;
            //Debug.DrawRay(pos, Back, Color.magenta);
            float yRot = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-GetAngle(Front, Back), yRot, GetAngle(Right, Left)), PitchingT * Time.deltaTime);
            
            objectToFLoat.position = Vector3.Lerp(pos, newPos, BouncingT * Time.deltaTime);
        }

        public float GetAngle(Vector3 positive, Vector3 negative)
        {
            if (positive.y > negative.y)
            {
                return Mathf.Atan((positive.y - negative.y) / Vector3.Distance(positive, negative)) * Mathf.Rad2Deg * SidePitchPower;
            }
            return -Mathf.Atan((negative.y - positive.y) / Vector3.Distance(positive, negative)) * Mathf.Rad2Deg * SidePitchPower;
        }

    }
}