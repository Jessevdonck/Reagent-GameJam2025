using System.Collections;
using UnityEngine;

public class ScreenEffectsManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup screenFlashCanvas;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private int flashCount = 3;

    private void Awake()
    {
        if (screenFlashCanvas != null)
        {
            screenFlashCanvas.alpha = 0;
        }
    }

    public void FlashScreen()
    {
        StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        for (int i = 0; i < flashCount; i++)
        {
            screenFlashCanvas.alpha = 1;
            yield return new WaitForSeconds(flashDuration);
            screenFlashCanvas.alpha = 0;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}