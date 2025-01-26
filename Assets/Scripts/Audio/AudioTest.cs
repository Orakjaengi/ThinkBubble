using UnityEngine;
using UnityEngine.Audio;

public class AudioTest : MonoBehaviour
{
    [SerializeField] private AudioResource testBGM;
    [SerializeField] private AudioResource testSFX;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioManager.Instance.PlayBGM(testBGM);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioManager.Instance.PlaySFX(testSFX);
        }
    }
}
