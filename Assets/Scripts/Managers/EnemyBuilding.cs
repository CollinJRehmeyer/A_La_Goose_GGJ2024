using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBuilding : MonoBehaviour
{
    public List<GameObject> floorObjs;
    public EnemyManager enemyManager;
    public int employeesInBuilding;
    public int numFloors;
    public float speed;
    public Vector3 moveDir;

    public bool destroyed;

    public StudioEventEmitter destroybuilding;

    void Start()
    {
        moveDir = transform.right * -1;
        destroybuilding = GetComponent<StudioEventEmitter>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir * speed;
    }

    //Just specifies a number of resumes to add to the resume stack after
    public void GenerateEmployees()
    {
        for(int i = 0; i < numFloors; i++)
        {
            int numEmployeesThisFloor = Random.Range(1, 7);
            employeesInBuilding += numEmployeesThisFloor;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Tank"))
        {
            //Debug.Log("My floors: " + numFloors + ", Tank Floors: " + (enemyManager.deptManager.newDeptIndex + 1));
            if(numFloors < (enemyManager.deptManager.newDeptIndex + 2))
            {
                DestroyBuilding();
            }
        }
    }

    public void DestroyBuilding()
    {
        destroyed = true;
        destroybuilding.Play();

        CameraShake.Instance.StartShake(0.3f, .01f);
        enemyManager.resumes.TryAddPagesToStack(employeesInBuilding);

        foreach (Animator a in GetComponentsInChildren<Animator>())
        {
            a.SetTrigger("falling");
            Fall();
 
        }
    }

    public void Fall()
    {
        moveDir = Vector3.down;
        speed =.03f;
        StartCoroutine(Die()); 
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(4);
        
        Destroy(gameObject);
    }
}
