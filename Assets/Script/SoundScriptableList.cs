using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundScriptableObject", menuName = "ScriptableObjects/NewSoundSOList")]
public class SoundScriptableList : ScriptableObject
{
    public List<SoundSO> SoundList;
}
