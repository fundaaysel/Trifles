using System;
using UnityEngine;

public class StartGame : MonoSingleton<StartGame>
{
    public void OnClick()
    {
        GameManager.ins.StartGame();
        Hide();
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}