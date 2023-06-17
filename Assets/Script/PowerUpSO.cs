using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpScriptableObject", menuName = "ScriptableObjects/NewPowerUpSO")]
public class PowerUpSO : ScriptableObject
{
    public PowerUpType PowerUptype;
    public int Damage;
}
