using UnityEngine;

public class CubeController : MonoBehaviour
{
     Vector3 CubeTarget;
    private float CubeSpeed = 30f; // made fast for testing purposes

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameObject)
            {
                CubeTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                CubeTarget.z = transform.position.z;
                CubeTarget.y = transform.position.y;

                transform.position = Vector3.MoveTowards(transform.position, CubeTarget, CubeSpeed * Time.deltaTime);

                Debug.Log("Cube at " + gameObject.name + " Moved.");
            }
        }
    }
}

