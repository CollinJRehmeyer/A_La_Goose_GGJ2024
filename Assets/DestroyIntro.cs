using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIntro : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.instance.StartGame();
        Destroy(gameObject);
    }
}
