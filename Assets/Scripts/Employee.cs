using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Cache;
using UnityEngine;

[Serializable]
public class Employee
{
    public Sprite employeeSprite;

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
    public float moraleLossPerLeechFactor =.05f;


    //Commented out to try and stop getting the serialization random number error
    public Employee()
    {
        //name = RandomName();

        //salary = UnityEngine.Random.Range(minSalary, maxSalary);
        //productivity = UnityEngine.Random.Range(minProductivity, maxProductivity);
        //likeability = UnityEngine.Random.Range(minLikeability, maxLikeability);
        //passion = UnityEngine.Random.Range(minPassion, maxPassion);
        //morale = 1 + passion;
        //moraleGainPerShip = passion/maxPassion * moraleGainPerShipFactor;
        //moraleLossPerLeech = (1-passion/maxPassion) * moraleLossPerLeechFactor;

        //totalEarned = 0;
        //yrsAtJob = 0;
    }

    public void InitilializeEmployee()
    {
        name = RandomName();

        salary = UnityEngine.Random.Range(minSalary, maxSalary);
        productivity = UnityEngine.Random.Range(minProductivity, maxProductivity);
        likeability = UnityEngine.Random.Range(minLikeability, maxLikeability);
        passion = UnityEngine.Random.Range(minPassion, maxPassion);
        morale = 1 + passion;
        moraleGainPerShip = passion / maxPassion * moraleGainPerShipFactor;
        moraleLossPerLeech = (1 - passion / maxPassion) * moraleLossPerLeechFactor;

        totalEarned = 0;
        yrsAtJob = 0;
    }

    public string RandomName()
    {
        string givenFirstHalf;
        string givenSecondHalf;

        string lastFirstHalf;
        string lastSecondHalf;

        string[] givenFirsts = { "Der", "Keen", "Coll", "Dav", "Ash", "Aar", "No", "Em", "For", "Isa", "Vio", "Gray", "Dan", "Mave", "Jay", "Jes", "Gin", "On", "Damn", "Bike"};
        string[] givenSeconds = { "nan", "son", "n", "e", "ly", "on", "ah", "ma", "ce", "bella", "let", "iel", "son", "rick", "den", "us", "g", "ix", "r" };

        //{ "Dorby", "Billmie", "Grundson", "Damn", "Blant", "Forson", "Biker", "Lorz" };

        string[] lastFirsts = { "Yorg", "Walt", "Rehy", "Mill", "Ander", "Thomp", "King", "Walker", "Mc", "Mac", "Smir", "Dav", "John", "Wil", "Martin", "Hernand", "Lop", "Pop", "Volk" };
        string[] lastSeconds = { "illiams", "meyer", "son", "er", "nix", "ly", "-Bling", "-Walker", "Beth", "Gyver", "nov", "is", "ez", "ov"};
            
            //{ "McSleeve", "Grunderson", "Raucous", "Blastronaut", "Ramrod", "Cussing" };

        string first = givenFirsts[UnityEngine.Random.Range(0, givenFirsts.Length)] + givenSeconds[UnityEngine.Random.Range(0, givenSeconds.Length)];
        string last = lastFirsts[UnityEngine.Random.Range(0, givenFirsts.Length)] + lastSeconds[UnityEngine.Random.Range(0, lastSeconds.Length)];

        return first + " " + last;
    }


    public void UpdateSalaryInfo()
    {
        totalEarned += salary;
        yrsAtJob++;
    }

    public void UpdateMorale()
    {
        float idealSalary = UnityEngine.Random.Range(50, 75);
        for (int i = 0; i < yrsAtJob; i++)
        {
            idealSalary += (idealSalary * 0.035f); //ideal salary is some starting salary plus an avg raise for each "year" they've worked
        }

        if(salary -  idealSalary < 0)
        {
            float toughItOutPcnt = (passion / 50) * 100;
            float loseMoraleChance = UnityEngine.Random.Range(0, 100f);

            if((int)loseMoraleChance > toughItOutPcnt)
            {
                morale -= UnityEngine.Random.Range(2, (2 + (10 - 10 * (passion / 50))));
            }
        }
    }

    public void Promote()
    {
        float percentRaise = UnityEngine.Random.Range(3f, 4f);
        salary += (salary * (percentRaise / 100));
        likeability += UnityEngine.Random.Range(5, 8) * (1 + (passion / 50));

        if(morale >= 50)
        {
            morale = 50;
        }
    }


    public string StatsString()
    {
        return name + "- " + "LossPerLeech: " + moraleLossPerLeech + ", Salary: " + salary + ", Prod: " + productivity + ", Likeability: " + likeability;
    }

}
