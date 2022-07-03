using UnityEngine;

public class FailMenu : MonoBehaviour
{
    private RectTransform _rt;

    private void Awake()
    {
        _rt = GetComponent<RectTransform>();
    }

    public void Show()
    {
        gameObject.SetActive(true);

    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnClick()
    {
        GameManager.ins.OnLevelFailed();
        Hide();
    }
}