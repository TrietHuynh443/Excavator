using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] TabController _tabController;
    [SerializeField] GameObject _content;

    public GameObject Content { get { return _content; } }

    private Image _background;
    public void OnPointerClick(PointerEventData eventData)
    {
        _tabController.OnSelect(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _tabController.OnEnter(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tabController.OnExit(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        _background = GetComponent<Image>();
        _tabController.Subscribe(this);
    }

    public void ChangeBackground(Sprite background)
    {
        _background.sprite = background;
    }
}
