using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// References 
// https://docs.unity3d.com/ScriptReference/GL.QUADS.html
// https://docs.unity3d.com/ScriptReference/Camera.ScreenToWorldPoint.html

public class Screen_Response : MonoBehaviour
{
    [SerializeField] Vector3 point1 = new Vector3(1f, 200f, 0f);
    [SerializeField] Vector3 point2 = new Vector3(1f, 1f, 0f);
    [SerializeField] Vector3 point3 = new Vector3(200f, 1f, 0f);
    [SerializeField] Vector3 point4 = new Vector3(200f, 200f, 0f);
    [Range(0f, 1920f)][SerializeField] float widthMax = 200f;
    [Range(0f, 1080)][SerializeField] float heightMax = 200f;


    float p1X, p2X, p3X, p4X;
    float p1Y, p2Y, p3Y, p4Y; 
    float p1Z, p2Z, p3Z, p4Z;
 
 
    public void RememberLastPositions()
    {
        p1X = point1.x;
        p2X = point2.x;
        p3X = point3.x;
        p4X = point4.x;

        p1Y = point1.y;
        p2Y = point2.y;
        p3Y = point3.y;
        p4Y = point4.y;

        p1Z = point1.z;
        p2Z = point2.z;
        p3Z = point3.z;
        p4Z = point4.z;

    }
  
   

    private Camera cam;

    void Start()
    {
        cam = Camera.main;

        // Laods the start values of vector3's
        RememberLastPositions();
       
    }

    private void Update()
    {
       
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse 0 down");
            if (Input.mousePosition.x > 0f && Input.mousePosition.x < widthMax && Input.mousePosition.y > cam.pixelHeight - heightMax && Input.mousePosition.y < cam.pixelHeight)
            {
                 Debug.Log("Definitely can click here");
            } 
            else
            {      
                Debug.Log("Cant Click here");
                if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log(Input.mousePosition);
                    point1.x = Input.mousePosition.x; 
                    point2.y = 1080f - Input.mousePosition.y ;
                    point4.x = Input.mousePosition.x - widthMax;
                    point1.y = 1080 -Input.mousePosition.y - heightMax;


                    





                    //if(widthMax + Input.mousePosition.x >= 1920f / 2f)
                    //{
                    //    widthMax -= -Input.mousePosition.x;
                    //}          

                    //if(widthMax + Input.mousePosition.x <= 1920f / 2f)
                    //{
                    //    widthMax += -Input.mousePosition.x;
                    //}
                    //if (Input.mousePosition.y >= 1080f / 2f)
                    //{
                        
                    //}
                    //if (Input.mousePosition.y <= 1080f / 2f)
                    //{
                    //    point2.y = -heightMax;
                    //}


                }
            }
        }
        else
        {
            Debug.Log("Mouse no longer down");
        }

    }

 

    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();
        
        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));
        var t = transform.localToWorldMatrix;

        //point3.x = widthMax;
        //point4.y = heightMax;

        // moves the corners evenly
        if (point1.y != p1Y)
        {
           // Mathf.Clamp(point1.y, 0f, cam.pixelHeight);

            point4.y = point1.y;
            p4Y = point4.y;
            p1Y = point1.y;
        }       

        if(point4.y != p4Y)
        {
           // Mathf.Clamp(point4.y, 0f, cam.pixelHeight);

            point1.y = point4.y;
            p1Y = point1.y;
            p4Y = point4.y;
        }

        if(point3.x != p3X)
        {
            //Mathf.Clamp(point3.x, 0f, cam.pixelWidth);

            point4.x = point3.x;
            p4X = point4.x;
            p3X = point3.x;
        }

        if(point4.x != p4X)
        {
           // Mathf.Clamp(point4.x, 0f, cam.pixelWidth);

            point3.x = point4.x;
            p3X = point3.x;
            p4X = point4.x;
        }

        if (point2.x != p2X)
        {
            point1.x = point2.x;
            p1X = point1.x;
            p2X = point2.x;
        }

        if (point1.x != p1X)
        {
            point2.x = point1.x;
            p2X = point2.x;
            p1X = point1.x;
        }

        if (point2.y != p2Y)
        {
            point3.y = point2.y;
            p3Y = point3.y;
            p2Y = point2.y;
        }

        if (point3.y != p3Y)
        {
            point2.y = point3.y;
            p2Y = point2.y;
            p3Y = point3.y;
        }

        Vector3 point11 = new Vector3(point1.x, point1.y, point1.z);
       Vector3 point22 = new Vector3(point2.x, point2.y, point2.z);
       Vector3 point33 = new Vector3(point3.x, point3.y, point3.z);
       Vector3 point44 = new Vector3(point4.x, point4.y, point4.z);

        //////////////////////////////////////////////
      

        // line drawer
        GL.LoadIdentity();
        GL.MultMatrix(t);
        GL.Begin(GL.QUADS);

        // loads points to line drawer to draw a square
        GL.Vertex3(point11.x, point11.y, point11.z);
        GL.Vertex3(point22.x, point22.y, point22.z);
        GL.Vertex3(point33.x, point33.y, point33.z);
        GL.Vertex3(point44.x, point44.y, point44.z);
        GL.End();

        // Draws Mouse and Camera stats
        GUILayout.BeginArea(new Rect(20, 20, 400, 200));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.Label("Mouse X: " + Input.mousePosition.x.ToString());
        GUILayout.Label("Mouse Y: " + Input.mousePosition.y.ToString());
        GUILayout.Label("Mouse Z: " + Input.mousePosition.z.ToString());
        GUILayout.EndArea();

    }


}

