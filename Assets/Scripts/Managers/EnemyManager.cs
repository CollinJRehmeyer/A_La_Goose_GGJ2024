using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] buildings;
    public GameObject buildingSpawnPoint;

    public DepartmentManager deptManager;
    public ResumeStack resumes;

    public float spawnInterval;
    public float timer = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        if(timer >= spawnInterval)
        {
            timer = 0;
            GenerateEnemyBuilding();
        }

        timer += Time.deltaTime;
    }

    public void GenerateEnemyBuilding()
    {
        GameObject g = buildings[Random.Range(0, buildings.Length)];
        GameObject newBuilding = Instantiate(g, buildingSpawnPoint.transform);

        int maxAdditionalFloors = deptManager.newDeptIndex + 1;

        int numFloors = 1 + Random.Range(0, maxAdditionalFloors + 1);

        EnemyBuilding eb = newBuilding.GetComponent<EnemyBuilding>();
        eb.numFloors = numFloors;
        eb.resumes = resumes;

        for(int i = 0; i < numFloors; i++)
        {
            eb.floorObjs[i].SetActive(true);
        }

        eb.GenerateEmployees();

    }

}
