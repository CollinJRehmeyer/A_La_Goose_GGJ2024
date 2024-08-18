using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoraleMeter : MonoBehaviour
{
    public EmployeeManager employeeManager;
    public WallMeter meter;
    public NumberTicker moraleTicker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meter.inputAmount = employeeManager.avgMorale;
        moraleTicker.value = employeeManager.avgMorale;
        //print("employee morale" + employeeManager.avgMorale);
    }
}
