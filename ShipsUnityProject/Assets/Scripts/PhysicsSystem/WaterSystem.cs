using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSystem : MonoBehaviour
{
    public static WaterSystem instance;

    public void Awake()
    {
        instance = this;
    }
    [Header ("Waves Chars")]
    public float WaveSpeed;
    public float WaveScale;
    public float WaveStrength;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public float GetWaterLine(Vector3 currentPosition, Vector3 offset)
    {
        //offset = currentPosition;
        Vector3 pos = (currentPosition);
        Vector3 UV = new Vector3(pos.x + Time.time * WaveSpeed, 0, pos.z + Time.time * WaveSpeed);
        Vector3 res = Vector3.up * Mathf.PerlinNoise((UV.x + offset.x) * WaveScale, (UV.z + offset.z) * WaveScale) * WaveStrength;
        return res.y;
    }
}
