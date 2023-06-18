using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/NewPlayerSO")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] private BulletSO _bulletSO;
    public BulletSO BulletSO => _bulletSO;
    public int Health;

}
