using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour, IPointerUpHandler
{
    [SerializeField] private string audioType;
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
        audioType = transform.parent.name;

        if (PlayerPrefs.HasKey(audioType))
        {
            volumeSlider.value = PlayerPrefs.GetFloat(audioType);
            AudioManager.Instance.SetAudioVolume(audioType, volumeSlider.value);
        }
        else
        {
            volumeSlider.value = 0.5f;
            AudioManager.Instance.SetAudioVolume(audioType, volumeSlider.value);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PlayerPrefs.SetFloat(audioType, volumeSlider.value);
        AudioManager.Instance.SetAudioVolume(audioType, volumeSlider.value);
    }
}
