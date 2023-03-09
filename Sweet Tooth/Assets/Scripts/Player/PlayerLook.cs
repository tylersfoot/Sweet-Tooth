using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public new Camera camera;
    private float xRotation = 0f;

    public float xSensitivity = 80f;
    public float ySensitivity = 60f;
    public float screenSizeFactor = 1f;

    void Start()
    {
        // get the base screen width and height
        int baseScreenWidth = 1920;
        int baseScreenHeight = 1080;

        // calculate the screen size factor
        screenSizeFactor = (float)Screen.width / baseScreenWidth + (float)Screen.height / baseScreenHeight;
        screenSizeFactor /= 2f;

        // adjust the sensitivity based on the screen size factor
        xSensitivity *= screenSizeFactor;
        ySensitivity *= screenSizeFactor;
    }

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;
        // calculate camera rotation for looking vertically
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        // apply to the camera transform
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // rotate player to look horizontally
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
