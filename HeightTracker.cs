// Charles Farris
// HeightTracker.cs for Unity
// Height tracker for a UI text element written in C# for Unity

// Built in classes
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

// Class-Object 
public class HeightTracker : MonoBehaviour 
{
	// Global Variables
	// Save Rocket position and size
    [SerializeField] Transform transform;
	// UI display text
    Text rocketHeight;

	// Start is called before the first frame
	void Start ()
    {
		// Initialize Rocket Height UI text object - component
        rocketHeight = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		// Update Rocket Height text on the UI with the objects y position
        rocketHeight.text = ((transform.position.y).ToString("0") + " m");
	}
}
