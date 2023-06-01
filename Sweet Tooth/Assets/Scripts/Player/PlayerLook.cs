using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public new Camera camera;
    private float verticalRotationTarget;
    private float verticalRotation;
    private float horizontalRotationTarget;
    private float horizontalRotation;

    void Start()
    {
        // adjust the sensitivity based on the screen size factor
        GameDataManager.Data.xSensitivity *= (float)Screen.width / 1920f;
        GameDataManager.Data.ySensitivity *= (float)Screen.height / 1080f;
    }

    void Update()
    {
        // vertical rotation
        verticalRotation = Mathf.Lerp(verticalRotation, verticalRotationTarget, Time.deltaTime * GameDataManager.Data.mouseSmoothing);
        camera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);


        // horizontal rotation
        horizontalRotation = Mathf.Lerp(horizontalRotation, horizontalRotationTarget, Time.deltaTime * GameDataManager.Data.mouseSmoothing);
        Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, horizontalRotation, transform.rotation.eulerAngles.z);
        transform.rotation = newRotation;

    }

    public void changeSensitivity(string axis, float sensitivity)
    {
        if (axis == "x")
        {
            GameDataManager.Data.xSensitivity = sensitivity;
            GameDataManager.Data.xSensitivity *= (float)Screen.width / 1920f;
        }
        else if (axis == "y")
        {
            GameDataManager.Data.ySensitivity = sensitivity;
            GameDataManager.Data.ySensitivity *= (float)Screen.height / 1080f;
        }
    }

    public void ProcessLook(Vector2 input)
    {
        // calculate camera rotation for looking vertically
        verticalRotationTarget -= (input.y * Time.deltaTime) * GameDataManager.Data.ySensitivity;
        verticalRotationTarget = Mathf.Clamp(verticalRotationTarget, -80f, 80f);
        horizontalRotationTarget += (input.x * Time.deltaTime) * GameDataManager.Data.xSensitivity;

    }
}
