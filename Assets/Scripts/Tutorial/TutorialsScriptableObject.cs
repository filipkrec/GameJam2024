using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Tutorials", menuName = "Scriptable Objects/Tutorials Scriptable Object")]
public class TutorialsScriptableObject : ScriptableObject
{
    [SerializeField] private List<TutorialData> TutorialsData;

    public string GetTutorialText(int _id)
    {
        string toReturn = null;
        if (TutorialsData.Exists((x) => x.Id == _id))
        {
            toReturn = TutorialsData.Find((x) => x.Id == _id).Text;
        }
        else
        {
            Debug.LogError("Invalid Tutorial Id : " + _id);
        }

        return toReturn;
    }
}

