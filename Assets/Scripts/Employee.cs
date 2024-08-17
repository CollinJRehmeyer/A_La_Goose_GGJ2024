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
    public int successfulShips;
    public float salary;
    public float productivity;
    public float likeability;
    public float morale;
    public float moraleLossPerLeech;
    public float moraleGainPerShip;
    public float passion;
    private float totalEarned;


    public float maxProductivity = 5;
    public float minProductivity = 0;
    public float maxLikeability = 5;
    public float minLikeability = 0;
    public float maxPassion = 5;
    public float minPassion = 0;
    public float maxSalary = 20;
    public float minSalary = 1;

    public float moraleGainPerShipFactor = 1;
    public float moraleLossPerLeechFactor =.01f;




    public Employee()
    {
        name = RandomName();

        salary = Random.Range(minSalary, maxSalary);
        productivity = Random.Range(minProductivity, maxProductivity);
        likeability = Random.Range(minLikeability, maxLikeability);
        passion = Random.Range(minPassion, maxPassion);
        morale = 1 + passion;
        moraleGainPerShip = passion/maxPassion * moraleGainPerShipFactor;
        moraleLossPerLeech = (1-passion/maxPassion) * moraleLossPerLeechFactor;

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
        likeability += Random.Range(5, 8) * (1 + (passion / 50));

        if(morale >= 50)
        {
            morale = 50;
        }
    }


    public string StatsString()
    {
        return name + "- " + "Age: " + yrsAtJob + ", Salary: " + salary + ", Prod: " + productivity + ", Likeability: " + likeability;
    }

}
