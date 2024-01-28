using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Victory Messages", menuName = "Scriptable Objects/Victory Messages Scriptable Object")]
public class VictoryMessagesScriptableObject : ScriptableObject
{
    public List<string> VictoryMessages;

    public string GetRandomMessage()
    {
        return VictoryMessages[Random.Range(0, VictoryMessages.Count)];
    }
}
