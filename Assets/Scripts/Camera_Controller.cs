using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Camera_Controller : MonoBehaviour
{
    public float speed = 3f;
    public float rotationSpeed = 55f;
    public float tiltSpeed = 55f;

    [SerializeField] private Text text_center;
    [SerializeField] private GameObject panel_text;
    [SerializeField] private Text text_panel;
    private Transform _cameraTransform;    

    public void Show_Text_Center(string text)
    {
        text_center.gameObject.SetActive(true);
        text_center.text = text;
        Invoke("Off_Text_Center", 7.0f);
    }

    private void Off_Text_Center() => text_center.gameObject.SetActive(false);

    public void Show_Text_Panel(string text)
    {
        text_panel.text = text;
        if (panel_text.activeSelf == false)
            panel_text.SetActive(true);
    }

    public void Use_Panel(bool show)
    {
        panel_text.SetActive(show);
    }

    private void Start()
    {
        if(text_center != null)
            text_center.gameObject.SetActive(false);
        if(panel_text != null)
            panel_text.SetActive(false);
            
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
        else if(Input.GetKey(KeyCode.LeftShift)){
            transform.Translate(new Vector3(0, -1 * speed * Time.deltaTime, 0));
        }    
    }

    private void HandleTilting()
    {
        if (Input.GetMouseButton(1)) // правая кнопка мыши + Shift Input.GetKey(KeyCode.LeftShift)
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

    public void Exit_Scene()=> SceneManager.LoadScene("Menu_Scene");

    
}
