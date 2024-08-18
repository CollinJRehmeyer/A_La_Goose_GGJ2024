using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeTicker : MonoBehaviour
{
    public EmployeeManager employeeManager;
    public NumberTicker totalPeopleTicker;
    public NumberTicker maxPeopleTicker;
    public NumberTicker totalCostTicker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        totalPeopleTicker.value = employeeManager.employees.Count;
        //TODO ADD DEPARTMENT * EMPLOYEES PER DEPT
        //totalPeopleTicker.value = 

        totalCostTicker.value = employeeManager.CalculateCost();
    }
}
