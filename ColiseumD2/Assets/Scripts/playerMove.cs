using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.Networking;

namespace Coliseum
{
    public class playerMove : MonoBehaviour
    {
        public float walkSpeed;
        //public float rotateSpeed;

        public string inputForward;
        public string inputBackward;
        public string inputLeft;
        public string inputRight;
        public string inputJump;
        public string inputRun;

        public CapsuleCollider playerCollider;
        public Vector3 jumpPower;
        private float distToGround;

        // Start is called before the first frame update
        void Start()
        {
            playerCollider = gameObject.GetComponent<CapsuleCollider>();
            distToGround = playerCollider.bounds.extents.y;
        }

        bool onGround()
        {
            return (Physics.CheckCapsule(playerCollider.bounds.center,
                end: new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f,
                    playerCollider.bounds.center.z), playerCollider.radius));
        }

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(inputForward))
            {
                if (Input.GetKey(inputRun))
                {
                    Vector3 dir;
                    dir = Camera.main.transform.forward;
                    dir.y = 0;
                    transform.Translate(dir * (walkSpeed * 1.30f), Space.Self);

                    //transform.Translate(0,0,walkSpeed * 2 * Time.deltaTime);
                }
                else
                {
                    Vector3 dir;
                    dir = Camera.main.transform.forward;
                    dir.y = 0;
                    transform.Translate(dir * walkSpeed, Space.Self);

                    //transform.Translate(0, 0, walkSpeed * Time.deltaTime);
                }
            }

            if (Input.GetKey(inputBackward))
            {
                Vector3 dir;
                dir = -Camera.main.transform.forward;
                dir.y = 0;
                transform.Translate(dir * walkSpeed, Space.Self);

                //transform.Translate(0,0,-(walkSpeed * 0.75f * Time.deltaTime));
            }

            if (Input.GetKey(inputRight))
            {
                Vector3 dir;
                dir = Camera.main.transform.right;
                dir.y = 0;
                transform.Translate(dir * walkSpeed, Space.Self);

                //transform.Rotate(0, rotateSpeed * Time.deltaTime,0);
            }

            if (Input.GetKey(inputLeft))
            {
                Vector3 dir;
                dir = -Camera.main.transform.right;
                dir.y = 0;
                transform.Translate(dir * walkSpeed, Space.Self);

                //transform.Rotate(0, -(rotateSpeed * Time.deltaTime),0);
            }

            if (IsGrounded() && Input.GetKeyDown(inputJump))
            {
                Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
                v.y = jumpPower.y;

                gameObject.GetComponent<Rigidbody>().velocity = jumpPower;

            }

            /*if (controller.isGrounded)
            {
                verticalVelocity = -gravity * Time.deltaTime;
                if (Input.GetKeyDown(inputJump))
                {
                    verticalVelocity = jumpForce;
                }
               
            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;
            }
           
            Vector3 moveVector = new Vector3(0,verticalVelocity,0);
            moveVector.x = Input.GetAxis("Horizontal") * 5.0f;
            moveVector.y = verticalVelocity;
            moveVector.z = Input.GetAxis("Vertical") * 9.0f;
            controller.Move(moveVector * Time.deltaTime);*/
        }
    }
}