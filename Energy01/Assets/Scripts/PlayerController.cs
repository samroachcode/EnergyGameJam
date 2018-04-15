using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Energy
{
    public class PlayerController : MonoBehaviour
    {

        public float walkSpeed = 8f;
        public float jumpSpeed = 7f;
        //public float jumpTime = 0.5f;
        public float increaseGravity; 


        private Rigidbody rb;
        private Collider thisCollider; 

        private bool pressedJump;

        private float distance;
        private float hAxis;

        private Vector3 movement;
        private Vector3 currPosition;
        private Vector3 newPosition;
        private Vector3 currentVelocity; 


        void Start()
        {
            pressedJump = false;
            rb = GetComponent<Rigidbody>();
            thisCollider = GetComponent<Collider>();
        }

        private void Update()
        {
            
        }

        void FixedUpdate()
        {
            Walk();
            Jump();
            CollisionCheck();
        }

        private void Walk()
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            // Distance ( speed = distance / time --> distance = speed * time)
            distance = walkSpeed * Time.deltaTime;
            hAxis = Input.GetAxis("Horizontal");
            // Movement vector
            movement = new Vector3(hAxis * distance, 0f, 0);
            currPosition = transform.position;
            newPosition = currPosition + movement;
            rb.MovePosition(newPosition);
        }

        private void Jump()
        {
            float jAxis = Input.GetAxis("Jump");
            bool isGrounded = CheckGrounded();
            if (jAxis > 0f)
            {
                if (!pressedJump && isGrounded)
                {
                    pressedJump = true;
                    Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);
                    rb.velocity = rb.velocity + jumpVector;
                }
            }
            else
            {
                pressedJump = false;
            }
            if (rb.velocity.y < 0)
                rb.velocity += Vector3.up * Physics.gravity.y * (increaseGravity - 1) * Time.deltaTime; 
        }

        bool CheckGrounded()
        {
            // Object size in x
            float sizeX = thisCollider.bounds.size.x;
            float sizeZ = thisCollider.bounds.size.z;
            float sizeY = thisCollider.bounds.size.y;

            // Position of the 4 bottom corners of the game object
            // We add 0.01 in Y so that there is some distance between the point and the floor
            Vector3 corner1 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
            Vector3 corner2 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, sizeZ / 2);
            Vector3 corner3 = transform.position + new Vector3(sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);
            Vector3 corner4 = transform.position + new Vector3(-sizeX / 2, -sizeY / 2 + 0.01f, -sizeZ / 2);

            // Send a short ray down the cube on all 4 corners to detect ground
            bool grounded1 = Physics.Raycast(corner1, new Vector3(0, -1, 0), 0.01f);
            bool grounded2 = Physics.Raycast(corner2, new Vector3(0, -1, 0), 0.01f);
            bool grounded3 = Physics.Raycast(corner3, new Vector3(0, -1, 0), 0.01f);
            bool grounded4 = Physics.Raycast(corner4, new Vector3(0, -1, 0), 0.01f);

            // If any corner is grounded, the object is grounded
            return (grounded1 || grounded2 || grounded3 || grounded4);
        }

        private void CollisionCheck()
        {
            currentVelocity = rb.velocity;
            
        }

    }
}
