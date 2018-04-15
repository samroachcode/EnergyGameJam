using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Energy
{
    public class PlayerController : MonoBehaviour
    {

        public float walkSpeed = 8f;
        public float jumpSpeed = 7f;
        public float jumpTime = 0.5f;
        public float increaseGravity; 


        private Rigidbody rb;
        private Collider thisCollider; 
        private bool pressedJump;

        void Start()
        {
            pressedJump = false;
            rb = GetComponent<Rigidbody>();
            thisCollider = GetComponent<Collider>();
        }

        void Update()
        {
            WalkHandler();
            JumpHandler();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("space pressed");
            }
        }

        void WalkHandler()
        {
            // Set x and z velocities to zero
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            // Distance ( speed = distance / time --> distance = speed * time)
            float distance = walkSpeed * Time.deltaTime;

            float hAxis = Input.GetAxis("Horizontal");
            //float vAxis = Input.GetAxis("Vertical");

            // Movement vector
            Vector3 movement = new Vector3(hAxis * distance, 0f, /*vAxis * distance*/0);

            Vector3 currPosition = transform.position;

            Vector3 newPosition = currPosition + movement;
            rb.MovePosition(newPosition);
        }

        void JumpHandler()
        {
            float jAxis = Input.GetAxis("Jump");
            bool isGrounded = CheckGrounded();
            if (jAxis > 0f)
            {
                if (!pressedJump && isGrounded)
                {
                    pressedJump = true;
                    //StartCoroutine(Jump());
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

        //IEnumerator Jump()
        //{
        //    rb.velocity = Vector3.zero;
        //    float timer = 0;
        //    float jAxis = Input.GetAxis("Jump");
           
        //    while (jAxis > 0f && timer < jumpTime)
        //    {
        //        //Calculate how far through the jump we are as a percentage
        //        //apply the full jump force on the first frame, then apply less force
        //        //each consecutive frame
        //        Vector3 jumpVector = new Vector3(0f, jumpSpeed, 0f);

        //        //Physics.gravity = new Vector3(0, Physics.gravity.y * 2);
        //        float proportionCompleted = timer / jumpTime;
        //        Vector3 thisFrameJumpVector = Vector3.Lerp(jumpVector, Vector3.zero, proportionCompleted);
        //        rb.AddForce(thisFrameJumpVector);
        //        timer += Time.deltaTime;
        //        yield return null;
        //    }

        //    pressedJump = false;
        //}

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
        
    }
}
