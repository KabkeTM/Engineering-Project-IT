using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensX;
    public float mouseSensY;
    

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        mouseSensX = (float)MenuVariables.mainSen * 10 * MenuVariables.resIndex;
        mouseSensY = (float)MenuVariables.mainSen * 10 * MenuVariables.resIndex;

    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensY * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
