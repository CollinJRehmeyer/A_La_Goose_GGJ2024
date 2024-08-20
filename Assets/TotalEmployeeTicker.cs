using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalEmployeeTicker : MonoBehaviour
{
    public NumberTicker ticker;
    public DepartmentManager deptMan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ticker.value = deptMan.employeesPerFloor*2 * (deptMan.newDeptIndex + 1);
    }
}
