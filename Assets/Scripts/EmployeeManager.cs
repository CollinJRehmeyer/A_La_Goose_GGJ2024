using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeManager : MonoBehaviour
{
    public List<Employee> employees = new List<Employee>();



    private void Start()
    {
        PopulateEmployeeList();
        PrintEmployees();
    }


    private void PopulateEmployeeList()
    {
        for (int i = 0; i < 10; i++)
        {
            employees.Add(new Employee());
        }
    }

    private void PrintEmployees()
    {
        foreach (Employee e in employees)
        {
            Debug.Log(e.StatsString());
        }
    }

}
