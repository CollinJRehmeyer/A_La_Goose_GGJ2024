using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] buildings;
    public GameObject buildingSpawnPoint;
    public GameObject currentBuilding;
    public float speed;

    public DepartmentManager deptManager;
    public ResumeStack resumes;

    public float spawnInterval;
    public float timer = 0;

    public float hitPoints;
    public HeadCannonAnimation cannon;

    private void Start()
    {
        cannon.cannonFired.AddListener(LoseHP);
    }

    private void Update()
    {
        if(timer >= spawnInterval)
        {
            timer = 0;
            currentBuilding = GenerateEnemyBuilding();
        }

        if(deptManager.tankMoving)
        {
            timer += Time.deltaTime;
        }

    }

    public GameObject GenerateEnemyBuilding()
    {
        hitPoints = 1;

        GameObject g = buildings[Random.Range(0, buildings.Length)];
        GameObject newBuilding = Instantiate(g, buildingSpawnPoint.transform);

        int maxAdditionalFloors = deptManager.newDeptIndex + 1;

        int numFloors = 1 + Random.Range(0, maxAdditionalFloors + 1);

        EnemyBuilding eb = newBuilding.GetComponent<EnemyBuilding>();
        eb.numFloors = numFloors;
        eb.enemyManager = this;
        eb.speed = speed;

        for(int i = 0; i < numFloors; i++)
        {
            eb.floorObjs[i].SetActive(true);
        }

        eb.GenerateEmployees();

        return newBuilding;

    }

    public void LoseHP()
    {
        hitPoints -= 1;

        if(hitPoints <= 0)
        {
            currentBuilding.GetComponent<EnemyBuilding>().DestroyBuilding();
        }
    }

}
