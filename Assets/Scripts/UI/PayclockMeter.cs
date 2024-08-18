using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayclockMeter : MonoBehaviour
{
    public EmployeeManager employeeManager;
    public GameManager gameManager;
    public RadialMeter meter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        meter.inputAmount = gameManager.payTimer / gameManager.PAY_TIMESCALE;
        print(meter.inputAmount + "   pay timer " + gameManager.payTimer + " / timescale: "+ gameManager.PAY_TIMESCALE );

    }
}
