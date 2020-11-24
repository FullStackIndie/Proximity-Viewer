using System.Collections.Generic;
using UnityEngine;

// No license no need to credit me personally
// Please keep the links where I borrowed code from to credit the original author's
public class CubeSelector : MonoBehaviour
{
    [Header("Cube Prefab References")]
    public GameObject cubePrefab; 
    public static List<GameObject> CubeList = new List<GameObject>();

    [Header("Cube Properties")]
    public static float maxDistance = 50f; // how far from camera will the mouse click register // Z-axis
    public float cubeSpeed = 2f; // how fast cube moves
    [SerializeField] private Color selectedCubeColor;
    [SerializeField] private int CurrentCubeInList = 0;
    [SerializeField] private Vector3 CubeSpawnPosition; // default is Vector3(0f, 0f, 0f)
    [SerializeField] private static int nameCounter = 1; // keeps track of how many cubes have been created
    
    void Update()
    {
        if (Input.GetKeyDown("right")) // cycles thru cubes
        {
            if (CurrentCubeInList >= CubeList.Count - 1)
            {
                CurrentCubeInList = 0;
            }
            else
            {
                CurrentCubeInList += 1;
            }
        }

        if (Input.GetKeyDown("left")) // cycles thru cubes
        {
           if (CurrentCubeInList <= 0)
            {
                if(CubeList.Count == 0)
                {
                    CurrentCubeInList = 0;
                }
                else
                {
                    CurrentCubeInList = CubeList.Count - 1;
                }
            }
            else
            {
                CurrentCubeInList -= 1;
            }
        }

        
        if (CubeList.Count >= 0)
        {
            foreach (GameObject cube in CubeList)
            {      
                CubeController cubeController = cube.GetComponent<CubeController>(); // Gets CubeController from cube

                if (CubeList.IndexOf(cube) != CurrentCubeInList)
                {
                    cube.GetComponent<Renderer>().material.color = cubeController.originalColor; // changes all cubes back to starting color
                }
                else
                {
                    cube.GetComponent<Renderer>().material.color = selectedCubeColor; // changes the color of selected cube
                }
                

                if (cube == CubeList[CurrentCubeInList] && Input.GetMouseButtonDown(0))
                { 
                    // gets mouse position and adjusts the Z-axis
                    cubeController.originalMousePosition = Input.mousePosition;
                    cubeController.originalMousePosition.z = maxDistance;

                    cubeController.cubeTargetPosition = Camera.main.ScreenToWorldPoint(cubeController.originalMousePosition);
                    // moves the cube to new destination // Time.deltaTime makes it smooth(framerate independent so its not jittery)
                    cube.transform.position = Vector3.MoveTowards(cube.transform.position, cubeController.cubeTargetPosition, cubeSpeed * Time.deltaTime); 
                    Debug.Log($"{cube.name} is now moving to new position at :  {cubeController.cubeTargetPosition}");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject thisCube; // Temp variable to access the instantiated prefab
            thisCube = Instantiate(cubePrefab, CubeSpawnPosition, Quaternion.identity);
            thisCube.name = $"Cube {nameCounter}";
            nameCounter++; // adds 1 to the name counter
            thisCube.GetComponent<CubeController>().enabled = true; // makes sure new cube can move
            CubeList.Add(thisCube); // adds new cube to list
            if(CubeList.Count > 1)
            {
                CurrentCubeInList = CubeList.Count -1; // updates the CurrentCube number // actual list starts at index 0 not at index 1
            }
            Debug.Log($"Added {thisCube.name} to the CubeList :: Current count is {CubeList.Count}");
        }
    }
}



