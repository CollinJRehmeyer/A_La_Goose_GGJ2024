using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EmployeeInspectWidget : MonoBehaviour
{
    public GameObject visContainer;
    public Employee selectedEmployee;
    public EmployeeButton selectedButton;
    
    public SpriteRenderer employeeSpriteRenderer;
    public TMP_Text salaryText;
    public TMP_Text prodText;
    public TMP_Text moraleText;
    public TMP_Text friendText;
    public TMP_Text shippedText;
    public TMP_Text nameText;

    public static EmployeeInspectWidget Instance { get; private set; }

    void Awake()
    {
        // Ensure that there is only one instance of EmployeeInspectWidget
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keeps the instance across scenes
        }
        else
        {
            Destroy(gameObject); // Prevents duplicates in the scene
        }
    }

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInfo();
    }
    public void UpdateInfo()
    {
        if (selectedEmployee != null)
        {
            employeeSpriteRenderer.sprite = selectedEmployee.employeeSprite;
            salaryText.text = "$" + selectedEmployee.salary.ToString("F2");
            prodText.text = selectedEmployee.productivity.ToString("F2") + "/" + selectedEmployee.maxProductivity.ToString("F0");
            moraleText.text = selectedEmployee.morale.ToString("F0") + "/" + "6";
            friendText.text = selectedEmployee.likeability.ToString("F0") + "/" + selectedEmployee.maxLikeability.ToString("F0");
            shippedText.text = selectedEmployee.successfulShips.ToString();
            nameText.text = selectedEmployee.name;
        }
    }
    public void SetEmployee(Employee employee)
    {
        selectedEmployee = employee;
        UpdateInfo();
        Show();
        
    }

    public void ClickSelectedButton()
    {
        selectedButton.SelectEmployee();
    }

    public void Show()
    {
        visContainer.SetActive(true);
    }

    public void Hide()
    {
        visContainer.SetActive(false);
    }

}
