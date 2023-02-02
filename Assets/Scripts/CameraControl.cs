using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Camera cam;
    public float cameraSpeed = 25;
    bool mouseMove = false;
    Vector3 mousePosition = new Vector3(0, 0, 0);

    private void Start()
    {
        cam = GetComponent<Camera>();
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetMouseButtonDown(1))
        {
            mouseMove = true;
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(1))
            mouseMove = false;


        Vector3 pos = transform.position;

        if (mouseMove)
        {
            Vector3 deltaPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - mousePosition;
            pos -= deltaPosition * Time.deltaTime * cameraSpeed;
        }




        if (mouseScroll < 0)
        {
            cam.orthographicSize -= mouseScroll * 10;
            cameraSpeed -= mouseScroll * 10;
        }
        else if (mouseScroll > 0 && cam.orthographicSize > 2)
        {
            cam.orthographicSize -= mouseScroll * 10;
            cameraSpeed -= mouseScroll * 10;
        }



        if (horizontal != 0 || vertical != 0)
        {
            pos.x += horizontal * Time.deltaTime * cameraSpeed;
            pos.y += vertical * Time.deltaTime * cameraSpeed;
        }

        transform.position = pos;
    }
}
