using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScreen : MonoBehaviour
{
    public GameObject blackScreen;

    public bool isOff = true;

    // Update is called once per frame
    public void Toggle()
    {

        Debug.Log("did it");
       if (isOff == true)
        {
            blackScreen.SetActive(true);
            isOff = false;
        } 

       else if (isOff == false)
        {
            blackScreen.SetActive(false);
            isOff = true;
        }
    }
}
