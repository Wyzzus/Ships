using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralNoiseProject
{
    public class FloatingComponent : EntityComponent
    {
        // Start is called before the first frame update
        public Transform objectToFLoat;
        public LayerMask RayMask;

        [Header("Pointe")]
        public float WaterLevel;
        public Vector3 Center;
        public Vector3 Left;
        public Vector3 Right;
        public Vector3 Front;
        public Vector3 Back;

        public float SidePitch;

        public Vector3[] Vertices;

        public MeshFilter TestMesh;

        public override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        void Update()
        {
            Floating();
        }

        public float GetWaterLevel()
        {


            return 0;
        }

        public void Floating()
        {
            Vector3 pos = transform.position;
            Left = GetVectorNoise(Left, -transform.right * 5);
            Right = GetVectorNoise(Right, transform.right * 5);
            SidePitch = Left.y - Right.y;

            Center = GetVectorNoise(Center, Vector3.zero);
            WaterLevel = Center.y * 50;

            transform.position = new Vector3(pos.x, WaterLevel, pos.z);

            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, SidePitch * 10), 10f);
        }

        public Vector3 GetVectorNoise(Vector3 original, Vector3 offset)
        {
            Vector3 vec = (original + offset) + Vector3.one * Time.deltaTime;
            return new Vector3(vec.x, new VoronoiNoise(10, 0.05f).Sample2D(vec.x, vec.z), vec.z);
        }

    }
}