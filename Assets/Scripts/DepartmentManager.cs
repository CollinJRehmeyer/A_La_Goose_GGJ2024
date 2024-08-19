using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartmentManager : MonoBehaviour
{
    public GameObject[] departmentObjs;
    public int newDeptIndex = 0;
    public int employeesPerFloor;
    public int maxEmployees = 6;

    public GameObject companyHead;

    public EmployeeManager employeeManager;
    public DeskButton acquireDeptButton;


    private void Start()
    {
        acquireDeptButton.onButtonPress.AddListener(AddDepartment);

        foreach (GameObject obj in departmentObjs)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        
    }


    public void AddDepartment()
    {
        if(newDeptIndex < departmentObjs.Length)
        {
            departmentObjs[newDeptIndex].SetActive(true);
            newDeptIndex++;
            SetMaxEmployees();
        }
    }

    public void SetMaxEmployees()
    {
        maxEmployees = 0;
        foreach(GameObject department in departmentObjs)
        {
            if(department.activeInHierarchy == true)
            {
                maxEmployees += 2 * employeesPerFloor;
            }
        }
    }
}
