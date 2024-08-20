using FMODUnity;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public EmployeeManager employeeManager;

    [Tooltip("How many seconds between paycycles")]
    public float PAY_TIMESCALE;

    [Tooltip("How many seconds between adding revenue")]
    public float ADD_VALUE_TIMESCALE;

    public float payTimer = 0, addValueTimer = 0;

    public Image overlay;

    public bool gameStarted;

    public StudioEventEmitter explodeSFX;

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

    private void Start()
    {
        Color c = overlay.color;
        c.a = 0;
        overlay.color = c;
    }


    void Update()
    {
        if(!employeeManager.gameLost)
        {
            Color c = overlay.color;
            c.a = 0;
            overlay.color = c;
        }

        if (gameStarted)
        {
            payTimer += Time.deltaTime;
            addValueTimer += Time.deltaTime;
        }


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
        //expectedValueReadout.text = "NetValuePerSecond: " + ev.ToString();
        //expectedValueTracker.transform.position = new Vector3(expectedValueTracker.transform.position.x, Mathf.LerpUnclamped(4, 5, ev/50), expectedValueTracker.transform.position.x);
        if (ev < 0)
        {
            var mainModule = expectedValueTracker.main;
            //mainModule.startColor = new Color(1, 0, 0);
        }
        else
        {
            var mainModule = expectedValueTracker.main;
            //mainModule.startColor = new Color(0, 1, 0);
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
        //print("Time To Ship: " + timeToShip + " :: Ships/s: " + shipsPerSecond + " :: rev/s: " + revenuePerSecond + " :: cost/s: " + costPerSecond);

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

    public void StartExplodeSFX()
    {
        explodeSFX.Play();
    }

    public void ExplodeAndReset()
    {
        StartExplodeSFX();
        StartCoroutine(Explode());
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(9f);

        StartCoroutine(ActivateOverlay());
    }

    public IEnumerator ActivateOverlay()
    {
        float timer = 0;

        while(timer < .05f)
        {
            float a = Mathf.Lerp(0, 1, timer / 0.05f);
            Color c = overlay.color;
            c.a = a;
            overlay.color = c;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        Color c2 = overlay.color;
        c2.a = 1;
        overlay.color = c2;

        Application.Quit();
    }


    public void StartGame()
    {
        gameStarted = true;
        employeeManager.fireButton.SetButtonActive(true);
        employeeManager.dismissButton.SetButtonActive(true);
        employeeManager.startProjectButton.SetButtonActive(true);

    }
}

