using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMeter : MonoBehaviour
{
    public bool alwaysUpdate;
    public float fullAmount = 1;
    public float fullAngle = 360;
    public float emptyAmount = 0;
    public float emptyAngle = 0;
    public float inputAmount;
    public Transform meterOrigin;
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialRotation = meterOrigin.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (alwaysUpdate)
        {
            UpdateMeterValue();
        }
    }
    void UpdateMeterValue()
    {
        float meterPercent = 0;
        if (fullAmount > emptyAmount)
        {
            meterPercent = (inputAmount - emptyAmount) / (fullAmount - emptyAmount);
            float targetAngle = Mathf.Lerp(emptyAngle, fullAngle, meterPercent);
            meterOrigin.rotation = initialRotation * Quaternion.Euler(emptyAngle, fullAngle, targetAngle);
            
        }
        else
        {
            meterPercent = (inputAmount - fullAmount) / (emptyAmount - fullAmount);
            float targetAngle = Mathf.Lerp(emptyAngle, fullAngle, meterPercent);
            meterOrigin.rotation = initialRotation * Quaternion.Euler(0, 0, targetAngle);

        }
    }
}
