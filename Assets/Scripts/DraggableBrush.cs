using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableBrush : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform _rectTransform;
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private Vector2 _originalPosition;

    [SerializeField] private int _cleanAmount = 10; // Количество чистоты за одно проведение щеткой
    [SerializeField] private float _cleanCooldown = 0.5f; // Задержка между применениями чистоты
    private float _lastCleanTime;

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
        _canvasGroup.alpha = 0.8f;
        _canvasGroup.blocksRaycasts = false;
        AudioManager.instance.StartShower();
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

        // Проверяем, находимся ли мы над питомцем
        var results = new System.Collections.Generic.List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        foreach (var result in results)
        {
            var pet = result.gameObject.GetComponent<PetClean>();
            if (pet != null && Time.time - _lastCleanTime >= _cleanCooldown)
            {
                pet.Clean(_cleanAmount);
                _lastCleanTime = Time.time;
                AnimManager.instance.SetState(AnimManager.instance.onWash);
                break;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
        _rectTransform.anchoredPosition = _originalPosition;
        AnimManager.instance.DefState();
        AudioManager.instance.StopShower(); 
    }
} 