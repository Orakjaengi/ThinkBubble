using UnityEngine;
using UnityEngine.UI;

public class ChoiceSelect : MonoBehaviour
{
    //Cat dies after x choices made!
    static int ChoicesMade = 0;

    public int Damage = 0;

    [SerializeField] AudioClip btnClickSound;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Select);
    }

    void Select()
    {
        AudioManager.Instance.PlaySFX(btnClickSound);

        ChoicesMade++;
        Debug.Log(Damage);
        BubbleManager.Instance.HandleDamage(Damage);

        DialogManager.Instance.InactivateDialog();
    }
}
