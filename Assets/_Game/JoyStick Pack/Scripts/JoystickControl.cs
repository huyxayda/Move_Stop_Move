using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickControl : MonoBehaviour
{
    public static Vector3 direct;
    private Vector3 screen;
    private Vector3 MousePosition => Input.mousePosition - screen / 2;

    private Vector3 startPoint;
    private Vector3 updatePoint;

    public RectTransform joystickBG;
    public RectTransform joystickControl;
    public float magnitude;
    public GameObject joystickPanel;
    void Awake()
    {
        screen.x = Screen.width;
        screen.y = Screen.height;

        direct = Vector3.zero;
        joystickPanel.SetActive(false);
    }


    void Update()
    {
        //co the dung joystick o bat cu diem nao tren man hinh
        //khi nhan chuot thi canvas joystick se hien tai diem nhan
        if (Input.GetMouseButtonDown(0))
        {
            startPoint = MousePosition;
            joystickBG.anchoredPosition = startPoint;
            joystickPanel.SetActive(true);

        }

        //khi keo chuot thi handle cua joystick se keo theo
        if (Input.GetMouseButton(0))
        {
            updatePoint = MousePosition;
            joystickControl.anchoredPosition = Vector3.ClampMagnitude((updatePoint - startPoint), magnitude) + startPoint;
            //dua toa do ve don vi chuan
            direct = (updatePoint - startPoint).normalized;
            //dua truc y ve truc z, vi direct se la 2d
            direct.z = direct.y;
            direct.y = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            joystickPanel.SetActive(false);
            direct = Vector3.zero;
        }
    }

    private void OnDisable()
    {
        //khi tat dieu khien direct cung phai bang 0
        direct = Vector3.zero;
    }
}
