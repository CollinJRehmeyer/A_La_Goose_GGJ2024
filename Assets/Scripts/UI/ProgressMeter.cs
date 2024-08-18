using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressMeter : MonoBehaviour
{
    public EmployeeManager employeeManager;
    public WallMeter meter;
    public NumberTicker goalTicker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        meter.inputAmount = employeeManager.totalProd;
        meter.fullAmount = employeeManager.prodGoal;
        goalTicker.value = employeeManager.prodReward;
    }
}
