using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public MeshFilter Mesh;

    public float WaveSpeed;
    public float WaveScale;
    public float WaveStrength;

    public Vector3[] vertPositions;
    public Vector3 offset;
    Vector3[] vertices;
    void Start()
    {
        vertPositions = Mesh.mesh.vertices;
        vertices = Mesh.mesh.vertices;
    }

    void FixedUpdate()
    {
        SetupChars();
        offset = transform.position;
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 pos = transform.TransformVector(vertPositions[i]);
            //Vector3 UV = new Vector3(pos.x + Time.time * WaveSpeed, 0, pos.z + Time.time * WaveSpeed);
            //Vector3 res = Vector3.up * Mathf.PerlinNoise((UV.x + offset.x) * WaveScale, (UV.z + offset.z) * WaveScale) * WaveStrength;
            //vertices[i].y = res.y;
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