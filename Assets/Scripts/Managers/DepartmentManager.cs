using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartmentManager : MonoBehaviour
{
    public GameObject[] departmentObjs;
    public int newDeptIndex = 0;
    public int employeesPerFloor;
    public int maxEmployees = 6;

    public float costToUpgrade;


    public bool tankMoving;
    public GameObject companyHead;
    public GameObject[] companyTreads; 

    public EmployeeManager employeeManager;
    public DeskButton acquireDeptButton;

    public StudioEventEmitter deptRealSound;
    public StudioEventEmitter deptFakeSound;


    private void Start()
    {
        acquireDeptButton.onButtonPress.AddListener(AcquireNewDepartment);

        foreach (GameObject obj in departmentObjs)
        {
            obj.SetActive(false);
        }

        SetMaxEmployees();
    }

    private void Update()
    {
        if(tankMoving)
        {
            foreach(GameObject trd in companyTreads)
            {
                trd.GetComponent<Animator>().SetBool("isMoving", true);
            }
        }
        else
        {
            foreach (GameObject trd in companyTreads)
            {
                trd.GetComponent<Animator>().SetBool("isMoving", false);
            }
        }

        if(employeeManager.totalValue >= costToUpgrade)
        {
            acquireDeptButton.SetButtonActive(true);
        }
        else
        {
            acquireDeptButton.SetButtonActive(false);
        }
    }


    public void AcquireNewDepartment()
    {
        if (newDeptIndex < departmentObjs.Length && employeeManager.totalValue >= costToUpgrade)
        {
            employeeManager.totalValue -= costToUpgrade;
            companyHead.GetComponent<Animator>().SetTrigger("department");
            StartCoroutine(AcquiringDept());

            deptFakeSound.Play();
            deptRealSound.Play();
            //////////////////////////////////////////////////////////////////////////////////////////////////////
        }
    }

    public IEnumerator AcquiringDept()
    {
        float animTime = 3.3f;
        float timer = 0;

        Vector3 startPos = companyHead.transform.localPosition;
        Vector3 newPos = new Vector3(companyHead.transform.localPosition.x, companyHead.transform.localPosition.y + 89.5f, companyHead.transform.localPosition.z);

        while (timer < animTime)
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
        if (newDeptIndex < departmentObjs.Length)
        {
            departmentObjs[newDeptIndex].SetActive(true);
            newDeptIndex++;
            SetMaxEmployees();
        }
    }

    public void SetMaxEmployees()
    {
        maxEmployees = 6;
        foreach (GameObject department in departmentObjs)
        {
            if (department.activeInHierarchy == true)
            {
                maxEmployees += 2 * employeesPerFloor;
            }
        }
    }
}
