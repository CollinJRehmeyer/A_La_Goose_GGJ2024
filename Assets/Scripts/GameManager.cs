using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameManager instance;

    public EmployeeManager employeeManager;

    [Tooltip("How many seconds between paycycles")]
    public float PAY_TIMESCALE;

    [Tooltip("How many seconds between adding revenue")]
    public float ADD_VALUE_TIMESCALE;

    private float payTimer = 0, addValueTimer = 0;

    public Slider payTimerSlider;
    public Slider addValueTimerSlider;
    public Text expectedValueReadout;
    public ParticleSystem expectedValueTracker;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }


    
    void Update()
    {
        payTimer += Time.deltaTime;
        addValueTimer += Time.deltaTime;

        payTimerSlider.value = payTimer / PAY_TIMESCALE;
        addValueTimerSlider.value = addValueTimer / ADD_VALUE_TIMESCALE;

        employeeManager.UpdateValues();

        if(payTimer >= PAY_TIMESCALE)
        {
            PayEmployees();
        }

        if(addValueTimer >= ADD_VALUE_TIMESCALE)
        {
            LeechValue();
        }
        float ev = CalculateExpectedValue();
        expectedValueReadout.text = "NetValuePerSecond: " + ev.ToString();
        expectedValueTracker.transform.position = new Vector3(expectedValueTracker.transform.position.x, Mathf.LerpUnclamped(4, 5, ev/50), expectedValueTracker.transform.position.x);
        if (ev < 0)
        {
            var mainModule = expectedValueTracker.main;
            mainModule.startColor = new Color(1, 0, 0);
        }
        else
        {
            var mainModule = expectedValueTracker.main;
            mainModule.startColor = new Color(0, 1, 0);
        }
        
    }

    public float CalculateExpectedValue()
    {
        float netValuePerSecond = 0.0f;
        float expectedProductionEachLeechCycle = employeeManager.CalculateProductivity();
        float leechCyclesToShip = employeeManager.prodGoal/expectedProductionEachLeechCycle;
        float timeToShip = leechCyclesToShip * ADD_VALUE_TIMESCALE;
        float shipsPerSecond = 1 / timeToShip;
        float revenuePerSecond = shipsPerSecond * employeeManager.prodReward;
        float costPerSecond = employeeManager.CalculateCost() / PAY_TIMESCALE;
        print("Time To Ship: " + timeToShip + " :: Ships/s: " + shipsPerSecond + " :: rev/s: " + revenuePerSecond + " :: cost/s: " + costPerSecond);

        return netValuePerSecond = revenuePerSecond-costPerSecond;
    }
    public void PayEmployees()
    {
        payTimer = 0;
        employeeManager.PayEmployeeSalaries();
    }
    public void LeechValue()
    {
        addValueTimer = 0;
        employeeManager.WorkEmployees();
    }
}

