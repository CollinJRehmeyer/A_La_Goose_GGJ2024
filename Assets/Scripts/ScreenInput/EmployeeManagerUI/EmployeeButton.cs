using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EmployeeButton : MonoBehaviour
{
    public Employee employee;
    public int employeeIndex;
    private Button button;

    private void Start()
    {

    }

    public void SelectEmployee()
    {
        Debug.Log(employee.StatsString());
        GameObject.FindObjectOfType<EmployeeManager>().selectedEmployee = employeeIndex;
    }
}
