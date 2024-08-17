using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeManager : MonoBehaviour
{
    public List<Employee> employees = new List<Employee>();

    public float totalProd;
    public float totalCost;
    public float avgMorale;

    public float revenue;

    public Slider prodSlider;
    public Slider costSlider;
    public Slider moraleSlider;
    public Text revenueText;

    public static GameStatsSO gameStats;
    private float timer = 0;

    [Range(0, 10)]
    public int selectedEmployee;

    private void Start()
    {
        PopulateEmployeeList();
        PrintEmployees();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;

            foreach (Employee e in employees)
            {
                e.UpdateSalaryInfo();
                e.UpdateMorale();
            }
            Debug.Log(employees[0].StatsString());

            UpdateValues();
        }
        

        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ResetEmployeeList();
            PrintEmployees();
        }
    }

    private void UpdateValues()
    {
        totalProd = CalculateProductivity();
        totalCost = CalculateCost();
        avgMorale = CalculateAvgMorale();

        prodSlider.value = totalProd / 1000;
        costSlider.value = totalCost / 1000;
        moraleSlider.value = avgMorale / 100;

        revenue += totalProd - totalCost;

        prodSlider.GetComponentInChildren<Text>().text = "Productivity: " + totalProd.ToString("F");
        costSlider.GetComponentInChildren<Text>().text = "Overhead: " + totalCost.ToString("F");
        moraleSlider.GetComponentInChildren<Text>().text = "Avg. Morale: " + avgMorale.ToString("F") + "/50";

        revenueText.text = "Revenue: " + revenue.ToString("F");
    }

    private void ResetEmployeeList()
    {
        employees.Clear();
        PopulateEmployeeList();
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


    private float CalculateProductivity()
    {
        float prod = 0;
        foreach(Employee e in employees)
        {
            prod += e.productivity;
        }
        return prod;
    }

    private float CalculateCost() 
    {
        float cost = 0;
        foreach (Employee e in employees)
        {
            cost += e.salary;
        }
        return cost;
    }

    private float CalculateAvgMorale()
    {
        float totalMorale = 0;
        foreach(Employee e in employees)
        {
            totalMorale += e.morale;
        }

        return (totalMorale / employees.Count);
    }
}
