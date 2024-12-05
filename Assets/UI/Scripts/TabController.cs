using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabController : MonoBehaviour
{
    private List<TabButton> tabButtons = new List<TabButton>();
    [SerializeField] private Sprite _selectedSprite;
    [SerializeField] private Sprite _hoverSprite;
    [SerializeField] private Sprite _normalSprite;

    private TabButton _currentSelected;
    public void Subscribe(TabButton button)
    {
        if (!tabButtons.Contains(button))
        {
            tabButtons.Add(button);
            OnSelect(button);
        }
    }

    public void OnEnter(TabButton tabButton)
    {
        if (tabButton != _currentSelected)
            tabButton.ChangeBackground(_hoverSprite);
    }

    public void OnExit(TabButton tabButton)
    {
        if (tabButton != _currentSelected)
            tabButton.ChangeBackground(_normalSprite);
    }

    public void OnSelect(TabButton tabButton)
    {
        _currentSelected = tabButton;

        DeselectAll();
        tabButton.ChangeBackground(_selectedSprite);
        tabButton.Content.SetActive(true);
    }

    private void DeselectAll()
    {
        foreach (TabButton button in tabButtons)
        {
            if (button != _currentSelected)
            {
                button.ChangeBackground(_normalSprite);
                button.Content.SetActive(false);
            }

        }
    }
}
