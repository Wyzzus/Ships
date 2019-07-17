using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public MeshFilter Mesh;

    public float WaveSpeed;
    public float WaveScale;
    public float WaveStrength;
    
    public Vector3 offset;
    Vector3[] vertices;
    void Start()
    {
        vertices = Mesh.mesh.vertices;
    }

    void FixedUpdate()
    {
        SetupChars();
        offset = transform.position;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 pos = transform.TransformVector(vertices[i]);
            vertices[i].y = WaterSystem.instance.GetWaterLine(pos, offset);
        }
        Mesh.mesh.vertices = vertices;
    }

    void SetupChars()
    {
        WaveSpeed = WaterSystem.instance.WaveSpeed;
        WaveScale = WaterSystem.instance.WaveScale;
        WaveStrength = WaterSystem.instance.WaveStrength;
    }

}