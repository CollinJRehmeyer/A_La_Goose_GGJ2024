using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashDisabler : MonoBehaviour
{
    public void DisableThis()
    {
        gameObject.SetActive(false);
    }
}
