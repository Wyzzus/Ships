using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    public static WeatherSystem instance;
    public PlayerController Player;

    [Header("Weather Chars")]
    public float WeatherSpeed;
    public float WeatherScale;



    public void Awake()
    {
        instance = this;
    }

    public int BeaufortScale = 0;
    public Vector2 Offset;

    public void SetBeaufortScale()
    {
        Vector2 pos = new Vector2(Player.transform.position.x, Player.transform.position.z);
        BeaufortScale = GetBeaufortScale(pos.x, pos.y);
    }

    public int GetBeaufortScale(float x, float y)
    {
        Vector2 UV = new Vector2(x + Time.time * WeatherSpeed, y + Time.time * WeatherSpeed);
        float BeaufortSample = Mathf.PerlinNoise((UV.x + Offset.x) * WeatherScale, (UV.y + Offset.y) * WeatherScale);
        return (int)(BeaufortSample * 12);
    }

    void Start()
    {
        Player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        
    }
}
