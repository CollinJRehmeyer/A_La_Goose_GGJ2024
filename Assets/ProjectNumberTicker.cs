using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectNumberTicker : MonoBehaviour
{
    public NumberTicker ticker;
    public EmployeeManager employeeManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ticker.value = employeeManager.totalProjectsCompleted + 1;
    }
}
