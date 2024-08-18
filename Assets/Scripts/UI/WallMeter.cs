using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMeter : MonoBehaviour
{
    public bool alwaysUpdate;
    public float fullAmount = 1;
    public float emptyAmount = 0;
    public float inputAmount;
    public Transform meterOrigin;
    // Start is called before the first frame update
    void Start()
    {
        
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
            meterOrigin.localScale = new Vector3(Mathf.Lerp(0, 1, meterPercent), meterOrigin.localScale.y, meterOrigin.localScale.z);
        }
        else
        {
            meterPercent = (inputAmount - fullAmount) / (emptyAmount - fullAmount);

            meterOrigin.localScale = new Vector3(Mathf.Lerp(1, 0, meterPercent), meterOrigin.localScale.y, meterOrigin.localScale.z);
        }
    }
}
