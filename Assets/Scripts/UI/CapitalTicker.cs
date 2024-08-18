using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalTicker : MonoBehaviour
{
    public EmployeeManager employeeManager;
    public NumberTicker capitalTicker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        capitalTicker.value = employeeManager.totalValue;
    }
}
