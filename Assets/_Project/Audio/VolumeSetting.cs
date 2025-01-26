using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using AudioMixerType;

public class VolumeSetting : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private AudioMixerGroupType audioType;
    [SerializeField] private string typeToString;
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
        typeToString = audioType.ToString();

        if (PlayerPrefs.HasKey(typeToString))
        {
            volumeSlider.value = PlayerPrefs.GetFloat(typeToString);
        }
        else
        {
            volumeSlider.value = 0.5f;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerPrefs.SetFloat(typeToString, volumeSlider.value);
        AudioManager.Instance.SetAudioVolume(typeToString, volumeSlider.value);
    }
}
