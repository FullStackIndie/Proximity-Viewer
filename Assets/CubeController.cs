using UnityEngine;

public class CubeController : MonoBehaviour
{
    CubeSelector cubeSelector; // creates a refenrence to cube spawner(CubeSelector)
    public Vector3 originalMousePosition; 
    public Vector3 cubeTargetPosition;
    private float speed;
   [SerializeField] private bool cubeKeepMoving = false;
    public Color originalColor;

    private void Awake()
    {
        originalColor = GetComponent<MeshRenderer>().material.color; // stores original color
        cubeSelector = FindObjectOfType<CubeSelector>(); // finds the 1st Cube Selector (should only be 1 per scene)
        speed = cubeSelector.cubeSpeed; // gets the speed from the cube spawner
    }

    void Update()
    {
        speed = cubeSelector.cubeSpeed;
        
        if (Input.GetMouseButtonDown(0))
        {
            if (!cubeKeepMoving)
            {
                // got some code from here, great resource
                // please keep the link to credit the author
                // https://gamedevbeginner.com/how-to-convert-the-mouse-position-to-world-space-in-unity-2d-3d/
                originalMousePosition = Input.mousePosition; // gets mouse position
                originalMousePosition.z = CubeSelector.maxDistance; // adjusts the Z-axis
                
                cubeTargetPosition = Camera.main.ScreenToWorldPoint(originalMousePosition);
                // moves from one position to another and stops at the final position
                transform.position = Vector3.MoveTowards(transform.position, cubeTargetPosition, speed * Time.deltaTime); // Thanks for showing me this, i didnt know about this method

                Debug.Log("Cube at " + gameObject.name + " Moved."); // can be deleted
                cubeKeepMoving = true;
            }
        }

        if (cubeKeepMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, cubeTargetPosition, speed * Time.deltaTime);
        }
    }
}

