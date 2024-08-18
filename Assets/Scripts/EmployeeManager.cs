using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeManager : MonoBehaviour
{
    public List<Employee> employees = new List<Employee>();
    public List<GameObject> employeeButtons = new List<GameObject>();

    public Sprite[] employeeSprites;

    public GameObject employeeGrid;
    public GameObject employeeBtnPrefab;

    public float totalProd;
    public float prodGoal = 100;
    public float prodReward = 1;
    public float totalCost;
    public float avgLikeability;
    public float avgMorale;
    public float totalValue;
    public float moraleBoostPership;
    public int numStartEmployees = 1;

    public Slider prodSlider;
    public Slider costSlider;
    public Slider likeabilitySlider;
    public Text valueText;
    public Text employeeLedgerText;
    public bool fire;
    public bool hire;



    public int selectedEmployee;

    private void Start()
    {
        PopulateEmployeeList();
        PrintEmployees();
    }

    private void Update()
    {


        if (fire)
        {
            if (selectedEmployee < employees.Count)
            {
                //employees.RemoveAt(selectedEmployee);
                RemoveEmployee(employees[selectedEmployee]);
            }
            fire = false;

        }
        if (hire)
        {
            Hire();
            hire = false;
            

        }
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ResetEmployeeList();
            PrintEmployees();
        }
    }

    public void UpdateValues()
    {
        if(totalProd >= prodGoal)
        {
            ShipProduct();
            totalProd = 0;
        }

        totalCost = CalculateCost();
        avgLikeability = CalculateAvgLikeability();

        prodSlider.value = totalProd / prodGoal;
        costSlider.value = totalCost / 5;
        //likeabilitySlider.value = avgLikeability / 5;

        prodSlider.GetComponentInChildren<Text>().text = "Progress To Ship: " + totalProd.ToString("F");
        costSlider.GetComponentInChildren<Text>().text = "Payroll Cost: " + totalCost.ToString("F");
        //likeabilitySlider.GetComponentInChildren<Text>().text = "Avg. Likeability: " + avgLikeability.ToString("F") + "/5";

        valueText.text = "Company Value: " + totalValue.ToString("F");
        //employeeLedgerText.text = EmployeeLedgerReadout();
    }
    private string EmployeeLedgerReadout()
    {
        string readout = "";
        int i=0;
        foreach (Employee e in employees)
        {
            readout += "#";
            readout += i.ToString() + " ";
            i++;
           
            readout += " || Prod: " + e.productivity.ToString("F1");
            readout += " || Sal: " + e.salary.ToString("F1");
            readout += " || Morale: " + e.morale.ToString("F1");
            readout += " || Likeability: " + e.likeability.ToString("F1");
            readout += " || Ships: " + e.successfulShips.ToString("F1");
            readout += " || Age: " + e.yrsAtJob.ToString("F1");
            readout += " || Name: " + e.name;
            readout += "\n";
        }
            return readout;
    }


    private void ResetEmployeeList()
    {
        employees.Clear();
        PopulateEmployeeList();
    }

    private void PopulateEmployeeList()
    {
        for (int i = 0; i < numStartEmployees; i++)
        {
            AddEmployee(new Employee(), i);
        }
    }

    public void AddEmployee(Employee e, int index)
    {
        employees.Add(e);
        GameObject empBtn = Instantiate(employeeBtnPrefab, employeeGrid.transform);
        EmployeeButton empBtnComp = empBtn.GetComponent<EmployeeButton>();
        employeeButtons.Add(empBtn);
        //empBtnComp.SetSprite(e.employeeSprite);
        empBtnComp.employee = e;
        empBtnComp.manager = this;
        empBtnComp.employeeIndex = index;
        empBtnComp.GetComponentInChildren<Text>().text = index.ToString();

    }

    public void RemoveEmployee(Employee e)
    {
        foreach(GameObject empBtn in employeeButtons)
        {
            if(empBtn.TryGetComponent<EmployeeButton>(out EmployeeButton empBtnComp))
            {
                if(empBtnComp.employee == e)
                {
                    employees.Remove(e);
                    employeeButtons.Remove(empBtn);
                    Destroy(empBtn);
                    return;
                }
            }
        }
        
    }



    public void Hire()
    {
        AddEmployee(new Employee(), employees.Count);
    }

    private void PrintEmployees()
    {
        foreach (Employee e in employees)
        {
            Debug.Log(e.StatsString());
        }
    }

    public void AddProgressToShip()
    {
        totalProd += CalculateProductivity();
        
    }
    public void WorkEmployees()
    {
        AddProgressToShip();
        foreach (Employee e in employees)
        {
            e.morale -= e.moraleLossPerLeech;
            if (e.morale < 0)
            {
                e.morale = 0;
            }
        }

    }


    public void PayEmployeeSalaries()
    {
        foreach (Employee e in employees)
        {
            totalValue -= e.salary;
        }
    }

    public float CalculateProductivity()
    {
        float prod = 0;
        foreach(Employee e in employees)
        {
            prod += e.productivity * avgLikeability * Mathf.Max(e.morale, 1f)  ;
        }
        return prod;
    }
    private void ShipProduct()
    {
        totalValue += prodReward;
        foreach (Employee e in employees)
        {
            e.successfulShips++;
            //every time you ship a product you feel good
            e.morale += e.moraleGainPerShip;
            if (e.morale > 5)
            {
                e.morale = 5;
            }
            e.morale+=e.moraleGainPerShip;
            //every time you ship a product, you feel a little less good about it, until you stop caring
            e.moraleGainPerShip -= (e.maxPassion - e.passion) * 0.2f;
            if (e.moraleGainPerShip < 0) {
                e.moraleGainPerShip = 0;
            }
            
        }
    }

    public float CalculateCost() 
    {
        float cost = 0;
        foreach (Employee e in employees)
        {
            cost += e.salary;
        }
        return cost;
    }

    private float CalculateAvgLikeability()
    {
        float totalLikeability = 0;
        foreach(Employee e in employees)
        {
            totalLikeability += e.likeability;
        }

        return (totalLikeability / employees.Count);
    }
}
