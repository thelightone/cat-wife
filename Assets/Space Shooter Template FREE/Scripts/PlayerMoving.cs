using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script defines the borders of 'Player's' movement. Depending on the chosen handling type, it moves the 'Player' together with the pointer.
/// </summary>

[System.Serializable]
public class Borders
{
    [Tooltip("offset from viewport borders for player's movement")]
    public float minXOffset = 1.5f, maxXOffset = 1.5f, minYOffset = 1.5f, maxYOffset = 1.5f;
    [HideInInspector] public float minX, maxX, minY, maxY;
}

public class PlayerMoving : MonoBehaviour {

    [Tooltip("offset from viewport borders for player's movement")]
    public Borders borders;
    [Tooltip("movement speed")]
    public float moveSpeed = 30f;
    
    Camera mainCamera;
    bool controlIsActive = true; 

    public static PlayerMoving instance; //unique instance of the script for easy access to the script

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        ResizeBorders();                //setting 'Player's' moving borders deending on Viewport's size
    }

    private void Update()
    {
        if (controlIsActive)
        {
            Vector3 targetPosition = Vector3.zero;
            bool hasInput = false;

            // Приоритет отдаем touch управлению
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                targetPosition = mainCamera.ScreenToWorldPoint(touch.position);
                hasInput = true;
            }
            // Если нет touch, используем мышь как fallback
            else if (Input.GetMouseButton(0))
            {
                targetPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                hasInput = true;
            }

            // Если есть ввод, перемещаем корабль
            if (hasInput)
            {
                targetPosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }

            // Ограничиваем движение границами
            transform.position = new Vector3
                (
                Mathf.Clamp(transform.position.x, borders.minX, borders.maxX),
                Mathf.Clamp(transform.position.y, borders.minY, borders.maxY),
                0
                );
        }
    }

    //setting 'Player's' movement borders according to Viewport size and defined offset
    void ResizeBorders() 
    {
        borders.minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x + borders.minXOffset;
        borders.minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y + borders.minYOffset;
        borders.maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x - borders.maxXOffset;
        borders.maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y - borders.maxYOffset;
    }

    public void SetControlActive(bool active)
    {
        controlIsActive = active;
    }
}
