using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberTicker : MonoBehaviour
{
    public double value;
    public int decimalsToShow;
    public bool alwaysUpdate;
    public TMP_Text tickerText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (alwaysUpdate)
        {
            UpdateTicker();
        }
    }
    public void UpdateTicker()
    {
        tickerText.text = value.ToString("N" + decimalsToShow);
    }
}
