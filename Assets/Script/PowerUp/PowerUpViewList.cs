using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpViewScriptableObject", menuName = "ScriptableObjects/NewPowerUpViewSO")]
public class PowerUpViewList : ScriptableObject
{
    public List<PowerUpView> PowerUps;
}

