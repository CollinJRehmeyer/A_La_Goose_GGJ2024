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
    public GameObject walkingSprite;

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

        if(walkingSprite.GetComponent<Image>().sprite != employee.employeeSprite)
        {
            walkingSprite.GetComponent<Image>().sprite = employee.employeeSprite;
        }

    }

    

    public void SelectEmployee()
    {
        if(manager.selectedEmployee == -1)
        {
            Debug.Log(employee.StatsString());
            manager.selectedEmployee = manager.employees.IndexOf(employee);
            SummonEmployeeToOffice();
        }
        else
        {
            Debug.Log("There is already a selected employee: " + manager.selectedEmployee);
        }
        
    }

    public void SummonEmployeeToOffice()
    {
        StartCoroutine(WalkToElevator());
    }
    
    public void DismissEmployeeFromOffice()
    {
        StartCoroutine(ReturnToWork());
    }

    public IEnumerator WalkToElevator()
    {
        button.image.enabled = false;
        button.interactable = false;

        while(walkingSprite.transform.position.x < manager.elevatorMarker.transform.position.x)
        {
            walkingSprite.transform.position += transform.right * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        walkingSprite.GetComponent<Image>().enabled = false;
    }

    public IEnumerator ReturnToWork()
    {
        walkingSprite.GetComponent<Image>().enabled = true;
        walkingSprite.transform.localScale = new Vector3(-1, 1, 1);

        while (walkingSprite.transform.position.x > transform.position.x)
        {
            walkingSprite.transform.position -= transform.right * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        walkingSprite.transform.localPosition = new Vector3(0,0,0);
        button.image.enabled = true;
        button.interactable = true;
        manager.selectedEmployee = -1;

        walkingSprite.transform.localScale = new Vector3(1, 1, 1);
    }

}
