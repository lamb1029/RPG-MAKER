using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    static public WeatherManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public ParticleSystem rain;

    private void Start()
    {
        
    }

    public void RainStart()
    {
        rain.Play();
    }

    public void RainStop()
    {
        rain.Stop();
    }

    public void RainDrop()
    {
        rain.Emit(12);

    }
}
