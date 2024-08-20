using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyViewScroll : MonoBehaviour
{
    GameObject uiToScroll;

    void Start()
    {
        uiToScroll = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            uiToScroll.transform.position += uiToScroll.transform.up * 0.01f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            uiToScroll.transform.position += uiToScroll.transform.up * -0.01f;
        }
    }
}
