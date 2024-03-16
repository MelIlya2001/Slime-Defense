using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{

    private Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)            //метод необходим, чтобы PanCamera() работал с перспективной камерой.
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.forward, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }


    [SerializeField] private Camera cam;

    private Vector3 dragOrigin;
    private Vector3 start_level;            //кастыли, ограничивающие передвижение камеры
    private Vector3 end_level;              //


    void Awake()
    {
        start_level = GameObject.Find("Start_level").transform.position;
        end_level = GameObject.Find("End_level").transform.position;
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        PanCamera();

    }



    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0))
            dragOrigin = GetWorldPositionOnPlane(Input.mousePosition, 0);

        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - GetWorldPositionOnPlane(Input.mousePosition, 0);

            //print("origin " + dragOrigin + "newPosition" + GetWorldPositionOnPlane(Input.mousePosition, 0) + " =difference " + difference);           для дебага


            if (difference.x + cam.transform.position.x <= start_level.x || difference.x + cam.transform.position.x >= end_level.x) return;

            cam.transform.position += new Vector3(difference.x, 0, 0);

        }
    }




}