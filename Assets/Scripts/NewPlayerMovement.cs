using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private Rigidbody rb;

    public float moveSpeed;
    private Vector3 moveDir;
    public Transform Orientation;
    private float horinp;
    private float vertinp;

    [Header("Jump")]
    public float JumpForce;

    [Header("drag")]
    public bool grounded;

    public LayerMask ground;

    [Header("animation")]
    public Animator PLanim;

    public bool running;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 currentspeed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            running = true;
        }
        else
        {
            running = false;
        }

        Inputfunction();
        Movement();
        Drag();
        Jump();
        //MovementAnim();
    }

    public void Inputfunction()
    {
        horinp = Input.GetAxisRaw("Horizontal");
        vertinp = Input.GetAxisRaw("Vertical");
    }

    private void Movement()
    {
        moveDir = Orientation.forward * vertinp + Orientation.right * horinp;
        rb.AddForce(moveDir.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void MovementAnim()
    {
        if (!running)
        {
            moveSpeed = 3f;
            if (horinp > 0 || horinp < 0)
            {
                PLanim.SetBool("StrafeRight", false);
                PLanim.SetBool("StrafeLeft", false);
                PLanim.SetBool("Run", false);
                PLanim.SetBool("Walk", true);
            }
            if (vertinp > 0f)
            {
                PLanim.SetBool("StrafeLeft", false);
                PLanim.SetBool("StrafeRight", false);
                PLanim.SetBool("Run", false);
                PLanim.SetBool("Walk", true);
            }
        }
        else
        {
            moveSpeed = 7f;
            if (horinp < 0)
            {
                PLanim.SetBool("Klaw", false);
                PLanim.SetBool("Walk", true);
                PLanim.SetBool("Run", true);
                PLanim.SetBool("StrafeLeft", true);
                PLanim.SetBool("StrafeRight", false);
            }
            else if (horinp > 0)
            {
                PLanim.SetBool("Klaw", false);
                PLanim.SetBool("Walk", true);
                PLanim.SetBool("Run", true);
                PLanim.SetBool("StrafeLeft", false);
                PLanim.SetBool("StrafeRight", true);
            }
            if (vertinp > 0f)
            {
                if (horinp == 0)
                {
                    PLanim.SetBool("Klaw", false);
                    PLanim.SetBool("Walk", true);
                    PLanim.SetBool("Run", true);
                    PLanim.SetBool("StrafeLeft", false);
                    PLanim.SetBool("StrafeRight", false);
                }
            }
        }
        if (vertinp == 0f && horinp == 0)
        {
            PLanim.SetBool("Klaw", false);
            PLanim.SetBool("Walk", false);
            PLanim.SetBool("Run", false);
            PLanim.SetBool("StrafeLeft", false);
            PLanim.SetBool("StrafeRight", false);
        }
        if (vertinp < 0f)
        {
            moveSpeed = 3f;
            PLanim.SetBool("StrafeLeft", false);
            PLanim.SetBool("StrafeRight", false);
            PLanim.SetBool("Run", false);
            PLanim.SetBool("Walk", false);
            PLanim.SetBool("Klaw", true);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    private void speedLimit()
    {
        Vector3 currentspeed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (currentspeed.magnitude > moveSpeed)
        {
            Vector3 limit = currentspeed.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limit.x, rb.linearVelocity.y, limit.z);
        }
    }

    private void Drag()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 1.5f, ground);
        if ((grounded))
        {
            rb.linearDamping = 7f;
        }
        else
        {
            rb.linearDamping = 0;
        }
    }
}