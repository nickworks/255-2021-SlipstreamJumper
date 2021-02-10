using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration;
    public float deceleration;
    private Vector3 velocity = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h != 0)
        { // user is pressing left or right (or both?)
            // applying acceleration to our velocity:
            velocity.x += h * Time.deltaTime * acceleration;
        }

        else // user is NOT pushing left or right:
        {
            if (velocity.x > 0) // player is moving right...
            {
                velocity.x -= deceleration * Time.deltaTime;
                if (velocity.x < 0) { velocity.x = 0; }
            }

            if (velocity.x < 0)
            {
                velocity.x += deceleration * Time.deltaTime;
                if(velocity.x > 0) { velocity.x = 0; }
            }
        }

        // applying velocity to our position:
        transform.position += velocity * Time.deltaTime;
    }
}
