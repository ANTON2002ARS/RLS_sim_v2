using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
   public float speed = 3f;
    public float rotationSpeed = 55f;
    public float tiltSpeed = 55f;
    
    private Transform _cameraTransform;
    

    private void Start()
    {
        _cameraTransform = GetComponentInChildren<Camera>().transform; // Находим transform камеры
    }

    private void Update()
    {
        HandleMovement();
        HandleTilting();        
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime);
        if(Input.GetKey(KeyCode.Space)){
            transform.Translate(new Vector3(0, 1 * speed * Time.deltaTime, 0));
        }       
    }

    private void HandleTilting()
    {
        if (Input.GetKey(KeyCode.LeftShift)) // правая кнопка мыши + Shift
        {
            Cursor.lockState = CursorLockMode.Locked;           
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            this.transform.Rotate(Vector3.right, -mouseY * tiltSpeed* Time.deltaTime); // инвертируем ось Y для удобства
            this.transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);

            // ограничение угла наклона (пример)
            //Vector3 eulerAngles = this.transform.localEulerAngles;
            //eulerAngles.x = Mathf.Clamp(eulerAngles.x, -80f, 80f); // ограничение от -80 до 80 градусов        
            //this.transform.localEulerAngles = eulerAngles;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Vector3 currentRotation = this.transform.localEulerAngles;
            this.transform.localRotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0f);
        }
    }

    
}
