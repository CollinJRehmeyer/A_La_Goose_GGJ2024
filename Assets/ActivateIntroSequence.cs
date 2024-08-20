using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateIntroSequence : MonoBehaviour
{
    public GameObject[] activateForIntro;
    public StudioEventEmitter bootSFX;
    bool done = false;

    public void ActivateScreen()
    {
        if (!done)
        {
            done = true;
            bootSFX.Play();
            foreach (GameObject go in activateForIntro)
            {
                go.SetActive(true);
            }
        }

    }
}
