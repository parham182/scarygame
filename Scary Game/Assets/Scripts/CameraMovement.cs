using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float cameraSens = 0.1f;
    [SerializeField] Transform playerBody;
    [SerializeField] float xClampMin = -90f;
    [SerializeField] float xClampMax = 90f;
    [SerializeField] RectTransform cameraTouchArea; // ناحیه قابل لمس برای دوربین

    private Vector2 lookInput;
    private float xRotation = 0f;
    private float yRotation = 0f;
    private bool isTouchingCameraArea = false;

    void Start()
    {
        xRotation = transform.localEulerAngles.x;
        if (xRotation > 180f) xRotation -= 360f;
        
        yRotation = playerBody != null ? playerBody.localEulerAngles.y : 0f;
    }

    void Update()
    {
        HandleTouchInput();
    }

    private void HandleTouchInput()
    {
        if (Touchscreen.current == null) return;

        var touch = Touchscreen.current.primaryTouch;
        
        if (touch.press.wasPressedThisFrame)
        {
            // بررسی آیا لمس در ناحیه دوربین است
            Vector2 touchPos = touch.position.ReadValue();
            if (IsTouchInCameraArea(touchPos))
            {
                isTouchingCameraArea = true;
            }
        }
        else if (touch.press.wasReleasedThisFrame)
        {
            isTouchingCameraArea = false;
        }

        if (isTouchingCameraArea && touch.press.isPressed)
        {
            Vector2 delta = touch.delta.ReadValue();
            lookInput = delta * cameraSens;
            MoveCamera();
        }
    }

    private bool IsTouchInCameraArea(Vector2 touchPosition)
    {
        if (cameraTouchArea == null) 
            return !IsPointerOverUI(); // اگر ناحیه مشخص نشده، هر جای غیر UI

        // تبدیل موقعیت لمس به مختصات ناحیه
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            cameraTouchArea, 
            touchPosition, 
            null, 
            out Vector2 localPoint
        );
        
        return cameraTouchArea.rect.Contains(localPoint);
    }

    private bool IsPointerOverUI()
    {
        if (EventSystem.current == null) return false;
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void MoveCamera()
    {
        yRotation += lookInput.x;
        xRotation -= lookInput.y;
        xRotation = Mathf.Clamp(xRotation, xClampMin, xClampMax);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        if (playerBody != null)
        {
            playerBody.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        }
    }
}