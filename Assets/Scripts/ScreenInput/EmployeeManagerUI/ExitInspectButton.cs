using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitInspectButton : MonoBehaviour
{
    public StudioEventEmitter click;
    public void DisableInspectWidget()
    {
        click.Play();
        transform.parent.gameObject.SetActive(false);
    }
}
