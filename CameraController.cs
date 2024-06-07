using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float rotationSpeedX = 0.05f;
    public float rotationSpeedY = 0.05f;

    private bool isTouching = false;
    private Vector2 touchStartPosition;

    private bool isTouchSupported = false;

    public static CameraController Instance { get; private set; }

    #region Singelton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
#if UNITY_EDITOR
        bool isTouchSupported = Input.touchSupported && SystemInfo.deviceModel.Contains("Tablet");
#else
        bool isTouchSupported = Input.touchSupported;
#endif
    }
    private void Update()
    {
        if (isTouchSupported)
        {
            HandleTouchInput();
        }
        else
        {
            HandleMouseInput();

        }



    }

    private void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (touch.position.y > Screen.height / 2f)
                    {
                        isTouching = true;
                        touchStartPosition = touch.position;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isTouching)
                    {
                        float rotationX = touch.deltaPosition.x * (rotationSpeedX * 100);
                        float rotationY = touch.deltaPosition.y * (rotationSpeedY * 100);

                        RotateCamera(rotationX, rotationY);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isTouching = false;
                    break;
            }
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.y > Screen.height / 2f)
            {
                isTouching = true;
                touchStartPosition = Input.mousePosition;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (isTouching)
            {
                float rotationX = (Input.mousePosition.x - touchStartPosition.x) * rotationSpeedX;
                float rotationY = (Input.mousePosition.y - touchStartPosition.y) * rotationSpeedY;

                RotateCamera(rotationX, rotationY);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isTouching = false;
        }
    }

    private void RotateCamera(float rotationX, float rotationY)
    {
        freeLookCamera.m_XAxis.Value += rotationX;
        freeLookCamera.m_YAxis.Value += rotationY;
    }
}
