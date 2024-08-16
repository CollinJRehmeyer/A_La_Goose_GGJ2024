using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee
{
    public string name;

    private float salary;
    private float productivity;
    private float morale;

    private float passion;


    public Employee()
    {
        name = RandomName();
        salary = Random.Range(0, 100);
        productivity = Random.Range(0, 100);
        morale = Random.Range(0, 100);

        passion = Random.Range(0, 50);
    }


    public string RandomName()
    {
        string[] firsts = { "Dorby", "Billmie", "Grundson", "Damn", "Blant", "Forson", "Biker", "Lorz" };
        string[] lasts = { "McSleeve", "Grunderson", "Raucous", "Blastronaut", "Ramrod", "Cussing" };

        string first = firsts[Random.Range(0, firsts.Length)];
        string last = lasts[Random.Range(0, lasts.Length)];

        return first + " " + last;
    }


    public string StatsString()
    {
        return name + "- " + "Salary: " + salary + ", Prod: " + productivity + ", Morale: " + morale;
    }

}
