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
            nameText.text = employee.name;
            prodText.text = "Prod: " + employee.productivity.ToString("F1");
            salText.text = "Sal: " + employee.salary.ToString("F1");
            //passionText.text = "Pass: " + employee.passion.ToString("F1");
        }
        else
        {
            print("Tried to populate page with no employee");
        }
    }
}
