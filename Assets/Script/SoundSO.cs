using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundScriptableObject", menuName = "ScriptableObjects/NewSoundSO")]
public class SoundSO : ScriptableObject
{
    public SoundType _soundType;
    public AudioClip _sound;
}
