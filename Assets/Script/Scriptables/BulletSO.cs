using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/NewBulletSO")]
public class BulletSO : ScriptableObject
{
    public float Speed;
    public int Damage;
}
