using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraZoom : MonoBehaviour
{
    int zoom = 20;
    int normal = 60;
    int conchetumare = 150;
    float smooth = 8;

    private bool isZoomed = false;

    public bool isConchetumare = false;


    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            isZoomed = true;
        }
        else if(Input.GetMouseButtonUp(1))
        {
            isZoomed = false;
        }

        if(isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, zoom, Time.deltaTime * smooth);
        }

        else if (!isZoomed)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isConchetumare = true;
        }

        else if(Input.GetKeyUp(KeyCode.F))
        {
            isConchetumare = false;
        }

        if (isConchetumare)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, conchetumare, Time.deltaTime * smooth);
        }

        else if (!isConchetumare)
        {
            GetComponent<Camera>().fieldOfView = Mathf.Lerp(GetComponent<Camera>().fieldOfView, normal, Time.deltaTime * smooth);
        }
    }
}
