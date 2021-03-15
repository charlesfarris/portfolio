// Charles Farris
// PlayerController.cs for Unity
// Farming Journey
// Move and rotate player from input

// Built in classes 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class definition
public class PlayerController : MonoBehaviour
{
    // Global Variables
    // Public variables are accessible in the editor
    // Untagged variables are private
    public float speed = 15;
    private Vector3 movement;
    Vector3 zeroVector = new Vector3(0, 0, 0);

    // Create variable to hold player animator
    Animator movementAnim;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize player position and animation components
        movement = transform.position;
        movementAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControls();
    }

    // Controls movement
    private void PlayerControls()
    {
        // Update movement vector from input manager
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement *= speed;

        // ROTATION

        // right 
        Quaternion right = Quaternion.Euler(0, 90, 0);
        // left
        Quaternion left = Quaternion.Euler(0, -90, 0);
        // forward
        Quaternion forward = Quaternion.Euler(0, 0, 0);
        // backward
        Quaternion back = Quaternion.Euler(0, 180, 0);

        // Line up Character rotation with positive/negative axis
        if (movement.z > 0)
        {
            // Turn straight
            this.transform.GetChild(0).rotation = forward;
            // Play running animation
            movementAnim.SetTrigger("run");
        }

        if (movement.z < 0)
        {
            // Turn back
            this.transform.GetChild(0).rotation = back;
            // Play running animation
            movementAnim.SetTrigger("run");
        }

        if (movement.x > 0)
        {
            // Turn right
            this.transform.GetChild(0).rotation = right;
            // Play running animation
            movementAnim.SetTrigger("run");
        }

        if (movement.x < 0)
        {
            // Turn left
            this.transform.GetChild(0).rotation = left;
            // Play running animation
            movementAnim.SetTrigger("run");
        }

        if (movement.x == 0 && movement.z == 0)
        {
            // Reset Animation
            movementAnim.Rebind();
            // Play idle animation
            movementAnim.SetTrigger("idle");
        }

        // Move the object that the script is attached to 
        transform.Translate(movement * Time.deltaTime);

    }
}
