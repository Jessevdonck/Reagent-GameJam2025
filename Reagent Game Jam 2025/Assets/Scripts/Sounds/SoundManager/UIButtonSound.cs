using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance?.PlayHover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance?.PlayClick();
    }
}