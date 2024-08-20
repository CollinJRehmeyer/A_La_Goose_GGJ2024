using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HeadCannonAnimation : MonoBehaviour
{
    public GameObject muzzleFlash;
    public UnityEvent cannonFired;
    public StudioEventEmitter shootInGame;

    private void Awake()
    {
        cannonFired = new UnityEvent();
    }

    public void EnableGO()
    {
        shootInGame.Play();

        muzzleFlash.SetActive(true);
        //CameraShake.Instance.StartShake(.5f, 0.05f);
        cannonFired.Invoke();
    }
}
