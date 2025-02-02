using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResumePage : MonoBehaviour
{
    public Employee employee;
    public TMP_Text nameText;
    public TMP_Text prodText;
    public TMP_Text salText;
    public TMP_Text passionText;

    public SpriteRenderer headshotRenderer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PopulatePage()
    {
        if (employee != null)
        {
            headshotRenderer.sprite = employee.employeeSprite;
            nameText.text = employee.name;
            prodText.text = employee.productivity.ToString("F1");
            salText.text = "$" + employee.salary.ToString("F2");
            passionText.text = employee.passion.ToString("F1");
        }
        else
        {
            print("Tried to populate page with no employee");
        }
    }
}