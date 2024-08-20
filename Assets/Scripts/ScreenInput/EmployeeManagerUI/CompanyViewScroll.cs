using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanyViewScroll : MonoBehaviour
{
    GameObject uiToScroll;
    private float lowerY;

    void Start()
    {
        uiToScroll = gameObject;
        lowerY = uiToScroll.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            uiToScroll.transform.position += uiToScroll.transform.up * 0.01f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            uiToScroll.transform.position += uiToScroll.transform.up * -0.01f;
        }

        Vector2 mouseScroll = Input.mouseScrollDelta;
        Vector3 scrollDelta3D = new Vector3(0, mouseScroll.y, 0);
        uiToScroll.transform.position += scrollDelta3D * 0.2f;

        if(uiToScroll.transform.position.y > lowerY)
        {
            uiToScroll.transform.position = new Vector3(uiToScroll.transform.position.x, lowerY, uiToScroll.transform.position.z);
        }
    }
}
