using System.Collections;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    [SerializeField] SpriteRenderer _interactable;
    [SerializeField] SpriteRenderer _unInteractable;

    void OnMouseDown()
    {
        StartCoroutine(SpriteFade(_interactable, 0, FrameController.Instance.FadeInDuration));
        StartCoroutine(FrameController.SpriteFade(_unInteractable, 1, FrameController.Instance.FadeInDuration));
    }

    IEnumerator SpriteFade(SpriteRenderer sr, float endValue, float duration)
    {
        float elapsedTime = 0;
        float startValue = sr.color.a;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAlpha);
            yield return null;
        }

        sr.gameObject.SetActive(false);
    }
}
