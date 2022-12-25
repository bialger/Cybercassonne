using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour
{
    private GameObject MainCamera;
    private float cameraVelocity;
    private float cameraRotationVelocity;
    private float cameraYMin;
    private float cameraXAngleMin;
    private float cameraXAngleMax;
    private KeyCode KeyUp1;
    private KeyCode KeyUp2;
    private KeyCode KeyDown1;
    private KeyCode KeyDown2;
    private KeyCode KeyLeft1;
    private KeyCode KeyLeft2;
    private KeyCode KeyRight1;
    private KeyCode KeyRight2;
    private KeyCode KeyForward1;
    private KeyCode KeyForward2;
    private KeyCode KeyBack1;
    private KeyCode KeyBack2;
    private KeyCode KeyRotateXUp1;
    private KeyCode KeyRotateXUp2;
    private KeyCode KeyRotateXDown1;
    private KeyCode KeyRotateXDown2;


    // Start is called before the first frame update
    void Start()
    {
        MainCamera = GameObject.Find("Main Camera");
        KeyUp1 = KeyCode.Q;
        KeyUp2 = KeyCode.PageUp;
        KeyDown1 = KeyCode.E;
        KeyDown2 = KeyCode.PageDown;
        KeyForward1 = KeyCode.W;
        KeyForward2 = KeyCode.UpArrow;
        KeyBack1 = KeyCode.S;
        KeyBack2 = KeyCode.DownArrow;
        KeyLeft1 = KeyCode.A;
        KeyLeft2 = KeyCode.LeftArrow;
        KeyRight1 = KeyCode.D;
        KeyRight2 = KeyCode.RightArrow;
        KeyRotateXUp1 = KeyCode.R;
        KeyRotateXUp2 = KeyCode.R;
        KeyRotateXDown1 = KeyCode.F;
        KeyRotateXDown2 = KeyCode.F;
        MainCamera.transform.position = new Vector3(0.0f, 8.0f, -8.0f);
        MainCamera.transform.rotation = Quaternion.Euler(50.0f, 0.0f, 0.0f);
        cameraVelocity = 6.0f;
        cameraRotationVelocity = 15.0f;
        cameraYMin = 3.0f;
        cameraXAngleMin = 5.0f;
        cameraXAngleMax = 85.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyForward1) || Input.GetKey(KeyForward2))
            MainCamera.transform.position += new Vector3(0.0f, 0.0f, cameraVelocity * Time.deltaTime);
        else if (Input.GetKey(KeyBack1) || Input.GetKey(KeyBack2))
            MainCamera.transform.position += new Vector3(0.0f, 0.0f, -cameraVelocity * Time.deltaTime);
        if (Input.GetKey(KeyUp1) || Input.GetKey(KeyUp2))
            MainCamera.transform.position += new Vector3(0.0f, cameraVelocity * Time.deltaTime, 0.0f);
        else if (Input.GetKey(KeyDown1) || Input.GetKey(KeyDown2))
            MainCamera.transform.position += new Vector3(0.0f, -cameraVelocity * Time.deltaTime, 0.0f);
        if (Input.GetKey(KeyLeft1) || Input.GetKey(KeyLeft2))
            MainCamera.transform.position += new Vector3(-cameraVelocity * Time.deltaTime, 0.0f, 0.0f);
        else if (Input.GetKey(KeyRight1) || Input.GetKey(KeyRight2))
            MainCamera.transform.position += new Vector3(cameraVelocity * Time.deltaTime, 0.0f, 0.0f);
        if (Input.GetKey(KeyRotateXUp1) || Input.GetKey(KeyRotateXUp2))
            MainCamera.transform.rotation = Quaternion.Euler(MainCamera.transform.rotation.eulerAngles + new Vector3(-cameraRotationVelocity * Time.deltaTime, 0.0f, 0.0f));
        else if (Input.GetKey(KeyRotateXDown1) || Input.GetKey(KeyRotateXDown2))
            MainCamera.transform.rotation = Quaternion.Euler(MainCamera.transform.rotation.eulerAngles + new Vector3(cameraRotationVelocity * Time.deltaTime, 0.0f, 0.0f));
        if (MainCamera.transform.position.y <= cameraYMin)
            MainCamera.transform.position = new Vector3(MainCamera.transform.position.x, cameraYMin, MainCamera.transform.position.z);
        if (MainCamera.transform.rotation.eulerAngles.x <= cameraXAngleMin)
            MainCamera.transform.rotation = Quaternion.Euler(cameraXAngleMin, 0.0f, 0.0f);
        if (MainCamera.transform.rotation.eulerAngles.x >= cameraXAngleMax)
            MainCamera.transform.rotation = Quaternion.Euler(cameraXAngleMax, 0.0f, 0.0f);
    }
}
