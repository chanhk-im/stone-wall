using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipHandler : MonoBehaviour
{
    public GameObject tooltipUI; // 상세 설명 UI

    void Start() {
        tooltipUI.SetActive(false);
    }

    // Pointer Enter 이벤트에 연결
    public void ShowTooltip(BaseEventData eventData)
    {
        tooltipUI.SetActive(true);
    }

    // Pointer Exit 이벤트에 연결
    public void HideTooltip(BaseEventData eventData)
    {
        tooltipUI.SetActive(false);
    }
}