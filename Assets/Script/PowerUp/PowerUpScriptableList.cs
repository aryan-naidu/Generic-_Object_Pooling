using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUpScriptableListObject", menuName = "ScriptableObjects/NewPowerUpSoList")]
public class PowerUpScriptableList : ScriptableObject
{
    public List<PowerUpSO> PowerUpSoList;
}

