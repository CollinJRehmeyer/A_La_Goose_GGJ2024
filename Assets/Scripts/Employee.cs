using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Cache;
using UnityEngine;

public class Employee
{
    public string name;
    public float yrsAtJob;
    public float salary;
    public float productivity;
    public float morale;

    private float passion;
    private float totalEarned;




    public Employee()
    {
        name = RandomName();

        salary = Random.Range(0, 100);
        productivity = Random.Range(0, 100);
        morale = Random.Range(0, 100);
        passion = Random.Range(30, 50);

        totalEarned = 0;
        yrsAtJob = 0;
    }


    public string RandomName()
    {
        string[] firsts = { "Dorby", "Billmie", "Grundson", "Damn", "Blant", "Forson", "Biker", "Lorz" };
        string[] lasts = { "McSleeve", "Grunderson", "Raucous", "Blastronaut", "Ramrod", "Cussing" };

        string first = firsts[Random.Range(0, firsts.Length)];
        string last = lasts[Random.Range(0, lasts.Length)];

        return first + " " + last;
    }


    public void UpdateSalaryInfo()
    {
        totalEarned += salary;
        yrsAtJob++;
    }

    public void UpdateMorale()
    {
        float idealSalary = Random.Range(50, 75);
        for (int i = 0; i < yrsAtJob; i++)
        {
            idealSalary += (idealSalary * 0.035f); //ideal salary is some starting salary plus an avg raise for each "year" they've worked
        }

        if(salary -  idealSalary < 0)
        {
            float toughItOutPcnt = (passion / 50) * 100;
            float loseMoraleChance = Random.Range(0, 100f);

            if((int)loseMoraleChance > toughItOutPcnt)
            {
                morale -= Random.Range(2, (2 + (10 - 10 * (passion / 50))));
            }
        }
    }

    public void Promote()
    {
        float percentRaise = Random.Range(3f, 4f);
        salary += (salary * (percentRaise / 100));
        morale += Random.Range(5, 8) * (1 + (passion / 50));

        if(morale >= 50)
        {
            morale = 50;
        }
    }


    public string StatsString()
    {
        return name + "- " + "Age: " + yrsAtJob + ", Salary: " + salary + ", Prod: " + productivity + ", Morale: " + morale;
    }

}
