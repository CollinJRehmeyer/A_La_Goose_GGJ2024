using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameStats")]
public class GameStatsSO : ScriptableObject
{
    public int MAX_MORALE_GAIN_ON_PROMOTE;
    public int MIN_MORALE_GAIN_ON_PROMOTE;

    public int MAX_MORALE_DECAY;
    public int MIN_MORALE_DECAY;
}
