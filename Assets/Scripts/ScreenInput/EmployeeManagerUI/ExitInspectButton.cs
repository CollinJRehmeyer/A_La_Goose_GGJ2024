using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitInspectButton : MonoBehaviour
{
    public void DisableInspectWidget()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
