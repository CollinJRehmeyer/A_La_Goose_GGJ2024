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
            payTimer = 0;
            employeeManager.PayEmployeeSalaries();
        }

        if(addValueTimer >= ADD_VALUE_TIMESCALE)
        {
            addValueTimer = 0;
            employeeManager.AddProdToValue();
        }
        
    }
}
