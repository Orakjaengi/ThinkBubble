using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitButton = GetComponent<Button>();
        exitButton.onClick.AddListener(Exit);
    }

    void Exit()
    {
        Application.Quit();
    }
}
