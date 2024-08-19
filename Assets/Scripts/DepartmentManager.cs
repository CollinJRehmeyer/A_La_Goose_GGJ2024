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
        acquireDeptButton.onButtonPress.AddListener(AcquireNewDepartment);

        foreach (GameObject obj in departmentObjs)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        
    }


    public void AcquireNewDepartment()
    {
        if (newDeptIndex < departmentObjs.Length)
        {
            companyHead.GetComponent<Animator>().SetTrigger("department");
            StartCoroutine(AcquiringDept());
        }
    }

    public IEnumerator AcquiringDept()
    {
        float animTime = 3.3f;
        float timer = 0;

        Vector3 startPos = companyHead.transform.localPosition;
        Vector3 newPos = new Vector3(companyHead.transform.localPosition.x, companyHead.transform.localPosition.y + 89, companyHead.transform.localPosition.z);

        while(timer < animTime)
        {
            companyHead.transform.localPosition = Vector3.Lerp(startPos, newPos, timer / animTime);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        companyHead.transform.localPosition = newPos;
        AddDepartment();
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
