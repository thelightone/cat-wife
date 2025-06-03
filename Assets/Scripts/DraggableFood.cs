using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableFood : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private Vector2 _originalPosition;

    private FoodItem _foodItem;

    [SerializeField] private int foodId;

    public static event Action onFeed;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();

        if (_canvasGroup == null)
        {
            _canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _originalPosition = _rectTransform.anchoredPosition;
        _canvasGroup.alpha = 0.6f;
        _canvasGroup.blocksRaycasts = false;
        AnimManager.instance.SetState(AnimManager.instance.beforeFood);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;

        // Проверяем, попали ли мы на питомца
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            var pet = result.gameObject.GetComponent<PetFeed>();
            if (pet != null)
            {
                pet.Feed(_foodItem, foodId);
                // Возвращаем еду на исходную позицию
                _rectTransform.anchoredPosition = _originalPosition;
                gameObject.SetActive(false);
                AnimManager.instance.SetStateWithTime(AnimManager.instance.afterFood,1);
                onFeed.Invoke();
                return;
            }
        }
        AnimManager.instance.DefState();
        // Если не попали на питомца, возвращаем еду на исходную позицию
        _rectTransform.anchoredPosition = _originalPosition;
    }

    public void InitValues(FoodItem foodItem)
    {
        _foodItem = foodItem;
    }
}