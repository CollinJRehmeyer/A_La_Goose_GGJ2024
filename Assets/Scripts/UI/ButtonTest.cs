using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTest : MonoBehaviour
{
    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(DebugButtonName);
    }

    public void DebugButtonName()
    {
        Debug.Log("Button Clicked: " + gameObject.name);
    }
}
