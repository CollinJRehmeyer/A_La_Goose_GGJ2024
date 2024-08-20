using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class BackgroundScrollManager : MonoBehaviour
{
    public static BackgroundScrollManager instance;
    public List<BackgroundScroll> bgsToScroll;
    
    public float[] speedMultipliers;
    public float baseSpeed;


    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
                   
        speedMultipliers = new float[bgsToScroll.Count];
    }

    private void Update()
    {
        for (int i = 0; i < bgsToScroll.Count; i++)
        {
            bgsToScroll[i].scrollSpeed = baseSpeed * speedMultipliers[i];
        }
    }
}
