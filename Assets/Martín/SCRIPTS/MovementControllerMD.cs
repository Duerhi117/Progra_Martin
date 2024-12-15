using UnityEngine;

namespace Martín
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private float crouchSpeed = 3;
        [SerializeField] private float walkSpeed = 5;
        [SerializeField] private float runSpeed = 7;
        [SerializeField] private float jumpForce = 1;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 0.2f;

        private Rigidbody rb;
        private bool isGrounded;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Move();
            CheckGround();
        }

        private void Update()
        {
            if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        private void Move()
        {
            Vector3 velocity = transform.rotation * new Vector3(HorizontalMove(), 0, VerticalMove()) * ActualSpeed();
            velocity.y = rb.velocity.y;
            rb.velocity = velocity;
        }


        private float ActualSpeed()
        {
            return IsRunning() ? runSpeed : IsCrouching() ? crouchSpeed : walkSpeed;
        }

        public float HorizontalMove()
        {
            return Input.GetAxis("Horizontal");
        }

        public float VerticalMove()
        {
            return Input.GetAxis("Vertical");
        }

        public bool IsMoving()
        {
            return HorizontalMove() != 0 || VerticalMove() != 0;
        }

        public bool IsRunning()
        {
            return Input.GetKey(KeyCode.LeftShift);
        }

        private bool IsCrouching()
        {
            return Input.GetKey(KeyCode.LeftControl);
        }

        private void Jump()
        {
            if (isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            }
        }

        private void CheckGround()
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
        }

        public bool IsGrounded()
        {
            return isGrounded;
        }
    }
}
