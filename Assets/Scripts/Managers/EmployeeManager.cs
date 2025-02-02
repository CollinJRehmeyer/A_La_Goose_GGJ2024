using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeManager : MonoBehaviour
{
    public List<Employee> employees = new List<Employee>();
    public List<GameObject> employeeButtons = new List<GameObject>();

    public bool gameLost = false;

    public DepartmentManager deptManager;

    public Sprite[] employeeSprites;
    public Sprite deathSprite;

    public GameObject employeeGrid;
    public GameObject employeeBtnPrefab;

    public GameObject elevatorMarker;

    public EmployeeInspectWidget employeeInspectWidget;

    public Animator elevator;

    public StudioEventEmitter fireEmployee;
    public StudioEventEmitter payEmployeeSound;
    public StudioEventEmitter makeMoneySound;

    public DeskButton fireButton;
    public DeskButton dismissButton;
    public DeskButton startProjectButton;
    public SpriteRenderer employeeOfficeSprite;

    public GameObject confetti;

    public StudioEventEmitter confettiSound;

    public GameObject employeeScreen;

    public SpeechBubble speechBubble;


    public float totalProd;
    public int totalProjectsCompleted = 0;
    public float prodGoal = 100;
    private float startingProdGoal = 150;
    public float prodReward = 1;
    private float startingProdReward;
    public int completedProjectsBeforeRewardIncrease;
    public float totalCost;
    public float avgLikeability;
    public float avgMorale;
    public float totalValue;
    public float moraleBoostPership;
    public int numStartEmployees = 1;
    public GameObject redAlarm;

    public Slider prodSlider;
    public Slider costSlider;
    public Slider likeabilitySlider;
    public Text valueText;
    public Text employeeLedgerText;
    public bool fire;
    public bool hire;
    public bool isWorkingOnProject;

    public int selectedEmployee;

    public float fireAnimDelay;
    public float fireRealDelay;

    public int completedProjects = 0;

    private void Start()
    {
        PopulateEmployeeList();
        PrintEmployees();

        fireButton.onButtonPress.AddListener(FireEmployee);
        dismissButton.onButtonPress.AddListener(DismissEmployee);
        startProjectButton.onButtonPress.AddListener(StartWorkingOnProject);
        startingProdGoal = prodGoal;
        startingProdReward = prodReward;

        FMOD.Studio.EventInstance musicEvent = GameObject.Find("Music").GetComponent<StudioEventEmitter>().EventInstance;
        musicEvent.setPaused(true);


    }

    private void Update()
    {
        if (totalValue < 0 && !redAlarm.activeInHierarchy)
        {
            redAlarm.SetActive(true);
            redAlarm.GetComponent<StudioEventEmitter>().Play();
        }
        else if (totalValue > 0 && redAlarm.activeInHierarchy)
        {
            redAlarm.SetActive(false);
        }

        if (fire)
        {
            if (selectedEmployee < employees.Count)
            {
                RemoveEmployee(employees[selectedEmployee]);
                CameraShake.Instance.StartShake(.5f, 0.05f);
            }
            fire = false;

        }
        if (hire)
        {
            Hire();
            hire = false;
            

        }
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            ResetEmployeeList();
            PrintEmployees();
        }
    }

    public void UpdateValues()
    {
        if(totalProd >= prodGoal)
        {
            ShipProduct();
            totalProd = 0;
            isWorkingOnProject = false;

        }

        totalCost = CalculateCost();
        avgLikeability = CalculateAvgLikeability();
        avgMorale = CalculateAvgMorale();
        prodSlider.value = totalProd / prodGoal;
        costSlider.value = totalCost / 5;
        //likeabilitySlider.value = avgLikeability / 5;

        prodSlider.GetComponentInChildren<Text>().text = "Progress To Ship: " + totalProd.ToString("F");
        costSlider.GetComponentInChildren<Text>().text = "Payroll Cost: " + totalCost.ToString("F");
        //likeabilitySlider.GetComponentInChildren<Text>().text = "Avg. Likeability: " + avgLikeability.ToString("F") + "/5";

        valueText.text = "Company Value: " + totalValue.ToString("F");
        //employeeLedgerText.text = EmployeeLedgerReadout();
    }
    private string EmployeeLedgerReadout()
    {
        string readout = "";
        int i=0;
        foreach (Employee e in employees)
        {
            readout += "#";
            readout += i.ToString() + " ";
            i++;
           
            readout += " || Prod: " + e.productivity.ToString("F1");
            readout += " || Sal: " + e.salary.ToString("F1");
            readout += " || Morale: " + e.morale.ToString("F1");
            readout += " || Likeability: " + e.likeability.ToString("F1");
            readout += " || Ships: " + e.successfulShips.ToString("F1");
            readout += " || Age: " + e.yrsAtJob.ToString("F1");
            readout += " || Name: " + e.name;
            readout += "\n";
        }
            return readout;
    }


    private void ResetEmployeeList()
    {
        employees.Clear();
        PopulateEmployeeList();
    }

    private void PopulateEmployeeList()
    {
        for (int i = 0; i < numStartEmployees; i++)
        {
            AddEmployee(new Employee(), i);
        }
    }

    public void AddEmployee(Employee e, int index)
    {
        employees.Add(e);
        GameObject empBtn = Instantiate(employeeBtnPrefab, employeeGrid.transform);
        EmployeeButton empBtnComp = empBtn.GetComponent<EmployeeButton>();
        employeeButtons.Add(empBtn);
        //empBtnComp.SetSprite(e.employeeSprite);
        empBtnComp.employee = e;
        empBtnComp.manager = this;
        empBtnComp.employeeIndex = index;
        empBtnComp.GetComponentInChildren<Text>().text = index.ToString();

    }

    public void RemoveEmployee(Employee e)
    {
        foreach(GameObject empBtn in employeeButtons)
        {
            if(empBtn.TryGetComponent<EmployeeButton>(out EmployeeButton empBtnComp))
            {
                if(empBtnComp.employee == e)
                {
                    employees.Remove(e);
                    employeeButtons.Remove(empBtn);
                    Destroy(empBtn);
                    return;
                }
            }
        }
        
    }

    public void DismissEmployee()
    {
        if (selectedEmployee != -1)
        {
            //promote them instead
            StartCoroutine(confettiStop());
            Employee e = employees[selectedEmployee];
            e.salary *= 1.2f;
            //e.morale += e.passion;
            e.morale = 5;
            speechBubble.gameObject.SetActive(false);

        }
        else
        {
            Debug.Log("No selected employee");
        }
        
    }

    public void FireEmployee()
    {
        if(selectedEmployee != -1)
        {
            Debug.Log("FIRED!");
            employeeInspectWidget.visContainer.SetActive(false);
            RemoveEmployee(employees[selectedEmployee]);
            selectedEmployee = -1;
            GameObject.Find("Doors").GetComponent<Animator>().SetTrigger("close");
            StartCoroutine(FireEmployeeFromCannon());
            speechBubble.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("No selected employee");
        }
    }

    public void SetEmployeeOfficeSprite(Sprite empSprite)
    {
        employeeOfficeSprite.sprite = empSprite;
    }

    public void Hire()
    {
        AddEmployee(new Employee(), employees.Count);
    }

    private void PrintEmployees()
    {
        foreach (Employee e in employees)
        {
            Debug.Log(e.StatsString());
        }
    }

    public void AddProgressToShip()
    {
        totalProd += CalculateProductivity();
        
    }
    public void StartWorkingOnProject()
    {
        if (!isWorkingOnProject)
        {
            BackgroundScrollManager.instance.baseSpeed = 0.5f;
            deptManager.tankMoving = true;

            FMOD.Studio.EventInstance musicEvent = GameObject.Find("Music").GetComponent<StudioEventEmitter>().EventInstance;
            musicEvent.setPaused(false);

            foreach (EnemyBuilding eb in FindObjectsOfType<EnemyBuilding>())
            {
                eb.speed = 0.01f;
            }


            isWorkingOnProject = true;
            float projectIncreaseFactor = Mathf.Floor(totalProjectsCompleted / completedProjectsBeforeRewardIncrease) * 1.5f;
            print("project increase factor: " + projectIncreaseFactor);
            prodGoal =  startingProdGoal + projectIncreaseFactor * startingProdGoal;
            prodReward = startingProdReward + projectIncreaseFactor * startingProdReward;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (avgMorale < 0.9f && employees.Count > 0)
        {
            StartCoroutine(Lose());
            StartCoroutine(speechMoraleDown());
        }

        if (avgMorale > 4.1f && employees.Count > 0)
        {
            StartCoroutine(Lose());
            StartCoroutine(speechMoraleUp());
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
    public void WorkEmployees()
    {
        if (isWorkingOnProject)
        {
            AddProgressToShip();
            foreach (Employee e in employees)
            {
                e.morale -= e.moraleLossPerLeech;
                if (e.morale < 0)
                {
                    e.morale = 0;

                }
                print("morale: " + e.morale);
            }
        }
        

    }

    private IEnumerator FireEmployeeFromCannon()
    {
        yield return new WaitForSeconds(fireAnimDelay);
        fireEmployee.Play();
        deptManager.companyHead.GetComponent<Animator>().SetTrigger("shoot");

        //real world sound
        yield return new WaitForSeconds(fireRealDelay);
        CameraShake.Instance.StartShake(1.6f, 0.2f);
    }

    public void PayEmployeeSalaries()
    {

        if(totalValue < 0)
        {
            if(!gameLost)
            {
                StartCoroutine(Lose());
                StartCoroutine(speechNoMoney());
            }
            return;
        }
        foreach (Employee e in employees)
        {
            payEmployeeSound.Play();
            totalValue -= e.salary;
        }
    }

    public float CalculateProductivity()
    {
        float prod = 0;
        foreach(Employee e in employees)
        {
            prod += e.productivity * avgLikeability * Mathf.Max(e.morale, 1f)  ;
        }
        return prod;
    }
    private void ShipProduct()
    {
        BackgroundScrollManager.instance.baseSpeed = 0f;
        deptManager.tankMoving = false;
        FMOD.Studio.EventInstance musicEvent = GameObject.Find("Music").GetComponent<StudioEventEmitter>().EventInstance;
        musicEvent.setPaused(true);
        completedProjects++;

        foreach(EnemyBuilding eb in FindObjectsOfType<EnemyBuilding>())
        {
            if(!eb.destroyed)
            {
                eb.speed = 0f;
            }
        }

        makeMoneySound.Play();


        totalValue += prodReward;
        totalProjectsCompleted++;
        foreach (Employee e in employees)
        {
            e.successfulShips++;
            //every time you ship a product you feel good
            e.morale += e.moraleGainPerShip;
            if (e.morale > 5)
            {
                e.morale = 5;
            }
            e.morale+=e.moraleGainPerShip;
            //every time you ship a product, you feel a little less good about it, until you stop caring
            e.moraleGainPerShip -= (e.maxPassion - e.passion) * 0.2f;
            if (e.moraleGainPerShip < 0) {
                e.moraleGainPerShip = 0;
            }
            
        }
    }

    public IEnumerator confettiStop()
    {
        //Debug.Log("Dismissed");
        confetti.SetActive(true);
        confettiSound.Play();
        yield return new WaitForSeconds(4);

        employeeButtons[selectedEmployee].GetComponent<EmployeeButton>().DismissEmployeeFromOffice();

        yield return new WaitForSeconds(10);
        confetti.SetActive(false);
    }

    public float CalculateCost() 
    {
        float cost = 0;
        foreach (Employee e in employees)
        {
            cost += e.salary;
        }
        return cost;
    }

    private float CalculateAvgLikeability()
    {
        float totalLikeability = 0;
        foreach(Employee e in employees)
        {
            totalLikeability += e.likeability;
        }

        return (totalLikeability / employees.Count);
    }
    public float CalculateAvgMorale()
    {

        if (employees.Count > 0)
        {
            float totalMorale = 0;
            foreach (Employee e in employees)
            {
                totalMorale += e.morale;
            }

            return ((totalMorale +2.5f) / (employees.Count +1));
        }
        else
        {
            return 0;
        }
    }

    public IEnumerator Lose()
    {
        gameLost = true;
        fireButton.canPress = false;
        dismissButton.canPress = false;
        startProjectButton.canPress = false;

        deptManager.companyHead.GetComponent<Animator>().SetTrigger("dead");
        GameManager.instance.ExplodeAndReset();

        yield return new WaitForSeconds(3);

        employeeScreen.SetActive(false);

        employeeOfficeSprite.sprite = deathSprite;

        GameObject.Find("Doors").GetComponent<Animator>().SetTrigger("open");


    }

    public IEnumerator speechMoraleUp()
    {
        yield return new WaitForSeconds(3);
        speechBubble.gameObject.SetActive(true);
        speechBubble.SetQuip("Your employees have.. UNIONIZED");
        
    }

    public IEnumerator speechMoraleDown()
    {
        yield return new WaitForSeconds(3);
        speechBubble.gameObject.SetActive(true);
        speechBubble.SetQuip("Your employees have rioted");

    }

    public IEnumerator speechNoMoney()
    {
        yield return new WaitForSeconds(3);
        speechBubble.gameObject.SetActive(true);
        speechBubble.SetQuip("You have committed the sin of being BROKE");

    }
}
