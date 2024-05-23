using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class playerLookAtMouse : MonoBehaviour
{
    public Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform PlayerTransform;
    [SerializeField] float rotationSpeed;
    [SerializeField] float mouseSensitivity;

    void Update()
    {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        PlayerTransform.localEulerAngles = new Vector3(yAxis.Value, PlayerTransform.localEulerAngles.y, PlayerTransform.localEulerAngles.z);
    }
}
