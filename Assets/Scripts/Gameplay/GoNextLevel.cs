using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoNextLevel : MonoBehaviour
{
    const string NextLevelText = "Press any key to spread more joy!";
    const string LastLevelText = "You won! Press any key to return to menu!";

    [SerializeField] private bool isLastLevel;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string targetScene;

    private void Start()
    {
        text.text = isLastLevel ? LastLevelText : NextLevelText;
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            SwitchScene();
        }
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene(targetScene);
    }
}
