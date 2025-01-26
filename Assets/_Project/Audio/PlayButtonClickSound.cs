using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayButtonClickSound : MonoBehaviour
{
    [SerializeField] AudioClip btnClickSound;
    [SerializeField] Button[] buttons;

    private void Start()
    {
        foreach (var button in buttons) 
        {
            button.onClick.AddListener(ClickButton);
        }
    }

    public void ClickButton()
    {
        AudioManager.Instance.PlaySFX(btnClickSound);
    }
}
