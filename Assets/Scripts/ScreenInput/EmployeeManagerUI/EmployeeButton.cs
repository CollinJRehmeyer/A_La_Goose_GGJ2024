using FMODUnity;
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
    public StudioEventEmitter elevator;

    public StudioEventEmitter click;
     

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

        if(walkingSprite.GetComponent<SpriteRenderer>().sprite != employee.employeeSprite)
        {
            walkingSprite.GetComponent<SpriteRenderer>().sprite = employee.employeeSprite;
        }

    }

    

    public void SelectEmployee()
    {
        EmployeeInspectWidget.Instance.selectedButton = this;
        if (manager.selectedEmployee == -1)
        {
            click.Play();
            if (EmployeeInspectWidget.Instance.selectedEmployee == employee)
            {
                manager.selectedEmployee = manager.employees.IndexOf(employee);
                SummonEmployeeToOffice();
            }
            else
            {
                EmployeeInspectWidget.Instance.SetEmployee(employee);
                Debug.Log(employee.StatsString());
            }
            
            
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
            walkingSprite.transform.position += transform.right * Time.deltaTime * 1.5f;
            yield return new WaitForEndOfFrame();
        }

        GameObject.Find("Doors").GetComponent<Animator>().SetTrigger("open");
        manager.speechBubble.gameObject.SetActive(true);
        manager.speechBubble.GenerateQuip();
        elevator = GameObject.Find("Doors").GetComponent <StudioEventEmitter>();
        elevator.Play();
        /////////////////////////////////////////////////////////////////
        walkingSprite.GetComponent<SpriteRenderer>().enabled = false;
        manager.SetEmployeeOfficeSprite(employee.employeeSprite);
    }

    public IEnumerator ReturnToWork()
    {
        GameObject.Find("Doors").GetComponent<Animator>().SetTrigger("close");
        elevator = GameObject.Find("Doors").GetComponent<StudioEventEmitter>();
        elevator.Play();
        walkingSprite.GetComponent<SpriteRenderer>().enabled = true;
        walkingSprite.GetComponent<SpriteRenderer>().flipX = true;

        while (walkingSprite.transform.position.x > transform.position.x)
        {
            walkingSprite.transform.position -= transform.right * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        walkingSprite.transform.localPosition = new Vector3(0,0,0);
        button.image.enabled = true;
        button.interactable = true;
        manager.selectedEmployee = -1;

        walkingSprite.GetComponent<SpriteRenderer>().flipX = false;
    }

}
