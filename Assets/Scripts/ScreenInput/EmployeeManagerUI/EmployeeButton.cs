using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class EmployeeButton : MonoBehaviour
{
    public Employee employee;
    public int employeeIndex;
    public Button button;
    public EmployeeManager manager;

    private void Start()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (button.image.sprite != employee.employeeSprite)
        {
            button.image.sprite = employee.employeeSprite;
        }

    }

    public void SelectEmployee()
    {
        Debug.Log(employee.StatsString());
        manager.selectedEmployee = manager.employees.IndexOf(employee);
    }

}
