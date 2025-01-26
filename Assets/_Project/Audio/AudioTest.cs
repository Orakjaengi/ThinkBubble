using UnityEngine;
using UnityEngine.Audio;

public class AudioTest : MonoBehaviour
{
    [SerializeField] private AudioResource testBGM;
    [SerializeField] private AudioClip testSFX;
    [SerializeField] private AudioClip testSFX2;
    [SerializeField] private AudioClip testSFX3;
    [SerializeField] private AudioResource testSFX4;


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
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AudioManager.Instance.PlaySFX(testSFX2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AudioManager.Instance.PlaySFX(testSFX3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AudioManager.Instance.PlaySFXLoop(testSFX4);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.Instance.StopSFXLoop();
        }
    }
}
