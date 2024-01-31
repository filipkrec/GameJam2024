using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private TextMeshPro text;
    [SerializeField] private TutorialsScriptableObject scriptableObject;

    [Header("Config")]
    [SerializeField] private int id;

    private void Awake()
    {
        text.text = scriptableObject.GetTutorialText(id);
    }
}
