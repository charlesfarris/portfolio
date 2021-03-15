// Charles Farris
// Follow.cs for Unity
// Attach to an object and set the target to follow

// Built in classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define object class
public class Follow : MonoBehaviour
{
    // Variable to hold target to follow
    [SerializeField] Transform target;
    // Distance away from target
    [SerializeField] float smooth = 2f;
    [SerializeField] int zDist = 0;
    [SerializeField] int yDist = 0;
    [SerializeField] int xDist = 0;
    
    // Update is called once per frame
    void Update ()
    {
        // Set endLocation for following object to update every frame
        Vector3 endLocation = new Vector3(target.position.x + xDist, target.position.y + yDist , target.position.z + zDist);
        // Set object position to the end location
        transform.position = Vector3.Lerp(transform.position, endLocation, Time.deltaTime * smooth);
	}
}
