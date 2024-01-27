using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sound Effects Scriptable Object", menuName = "Scriptable Objects/New Sound Effects Scriptable Object")]
public class SoundEffects : ScriptableObject
{
    [Serializable]
    private class SoundEffectData
    {
        public SoundEffectType Type = SoundEffectType.None;
        public AudioClip[] Clips = null;
    }

    [SerializeField] private List<SoundEffectData> _soundEffects = new List<SoundEffectData>();

    public AudioClip GetSoundEffectByType(SoundEffectType type)
    {
        SoundEffectData targetData = _soundEffects.Find((data) => data.Type == type);
        if (targetData != null && targetData.Clips.Length >= 1) return targetData.Clips[targetData.Clips.Length > 1 ? UnityEngine.Random.Range(0, targetData.Clips.Length) : 0];
        else return null;
    }
}

public enum SoundEffectType
{
    None,
    UISelect,
    UIChangeSelection,
    // TODO: add missing types
}