using System.Collections.Generic; // need this to use List<>
using System.Threading.Tasks;
using UnityEngine;

// this script should be on your Camera and your camera should have a collider on it
// feel free to change, edit, distribute this code
// The main problems with this script is that if you have over 900 gameobjects in close proximity you get a slight lag if your paying attention/ not much
// On a lower end device this may cause more lag so take this into consideration
// maybe dont call this in an update loop but call it from a Coroutine or Event every 5 or 10 seconds with a big camera radius
public class ProximityViewer : MonoBehaviour
{
    // how far the camera will search for objects
    float cameraSearchRadius = 20f;

    // how close does the gameobject need to be before its visible
    float closeDistance = 5f;
    // how faraway from the camera does the gameobject need to be before turning off renderer;
    float farDistance = 30f;
    // this is the result of 2 vector3 subtracted from one another
    float distanceBetween;
    // List to store gameobjects we have prevoiusly hit with the camera
    List<GameObject> hitObjs = new List<GameObject>();

    //////////////////////////////////////////////
    // Camera movement Variables from Fuzzy Logic
    // https://gamedev.stackexchange.com/questions/104693/how-to-use-input-getaxismouse-x-y-to-rotate-the-camera

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;
    ///////////////////////////////////////////

    // Update is called once per frame
    void Update()
    {
        // Scan from the position of the camera(assuming this script is attached to the camera)
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, cameraSearchRadius); // Unity Docs OverlapSphere
        foreach (var hitCollider in hitColliders)
        {
            // checks list to see if the current gameobject that was hit exists
            if (hitObjs.Contains(hitCollider.gameObject))
            {
                Debug.Log($"Already contains {hitCollider.gameObject.name}");
            }
            else
            {
                // adds hit gameobject to the list
                hitObjs.Add(hitCollider.gameObject);
            }
        }

            // searches the list of scanned gameobjects
            foreach (GameObject go in hitObjs)
            {
                // makes sure the gameobject that is stored isnt the camera(assuming this script is attached to the camera)
                if (go != this.gameObject)
                {
                    // gets the distance between the camera(assuming script is attached to camera) and gameobject that has been prevouisly scanned
                    distanceBetween = Vector3.Distance(transform.position, go.transform.position);
                    // if the distnace between camera and gameobject is greater than the closeDistance
                    if (distanceBetween > closeDistance)
                    {
                        // turns on the gameobject renderer because it is farther than the closeDistnace from the camera and within the cameras Scanning radius
                        go.gameObject.GetComponent<Renderer>().enabled = true;

                        // You can substitute <Renderer> for the <MeshRenderer>
                        // go.GetComponent<MeshRenderer>().enabled = true;
                    }
                    if (distanceBetween > farDistance)
                    {
                        // turns off the gameobject renderer because it is farther than the farDistance specified
                        go.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                }
            }


        //////////////////////////////////////////////
        // Got the camera mouse movement code from stack exchange
        // Username : Fuzzy Logic
        // https://gamedev.stackexchange.com/questions/104693/how-to-use-input-getaxismouse-x-y-to-rotate-the-camera
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        /////////////////////////////////////////////////////////////////////////////////


        // basic camera movement
        if (Input.GetKey(KeyCode.W))
        {
            transform.localPosition += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.localPosition += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.localPosition += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.localPosition += new Vector3(1, 0, 0);
        }

    }
}
