using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateBtnWithRandEmp : MonoBehaviour
{
    public EmployeeManager manager;

    void Start()
    {
        Employee employee = new Employee();
        employee.employeeSprite = manager.employeeSprites[Random.Range(0, manager.employeeSprites.Length)];
        employee.InitilializeEmployee();
        GetComponent<EmployeeButton>().employee = employee;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
