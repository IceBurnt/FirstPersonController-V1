using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float capSpeed;
    public float groundDrag;

    [Header("Ground check")]
    public float groundCheckDistance;
    public LayerMask whatIsGround;
    public bool grounded;

    [Header("Jumping")]
    public float jumpForce;
    public float airMultiplier;
    private bool canJump = true;

    [Header("Sliding")]
    public float slideSpeedMultiplier = 2f;
    public float slideDuration = 0.7f;
    public float slideCooldown = 1f;

    private bool isSliding = false;
    private bool canSlide = true;
    private float originalCapSpeed;

    public Slider cooldownSlider; // Reference to the UI Slider

    [Header("Crouching")]
    public float crouchSpeedMultiplier = 0.3f;
    public float crouchColliderHeightMultiplier = 0.5f;

    private bool isCrouching = false;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

  

    [Header("Universal")]
    public CapsuleCollider playerCollider;
    public float slideColliderHeightMultiplier = 0.6f;
    private float originalColliderHeight;
    private float originalMoveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public LayerMask Vehicle;

    Vector3 moveDirection;

    Rigidbody rb;

    private Trash trashScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        originalCapSpeed = capSpeed;
        originalColliderHeight = playerCollider.height;
        originalMoveSpeed = moveSpeed;

        trashScript = FindObjectOfType<Trash>(); 

        if (cooldownSlider != null)
        {
            cooldownSlider.highValue = slideCooldown; 
            cooldownSlider.lowValue = slideCooldown;    
        }
    }

    private void Update()
    {
        //Function calls
        Inputs();
        SpeedControl();
        GroundCheck();
        Jump();
        //Slide();
        //Crouch();
    }

    private void FixedUpdate()
    {
        XZmovement();
    }

    private void Inputs() // ------------------------------------------------------------------------------------
    {
        //XZ vector math, imported package manager addon
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void XZmovement() // ------------------------------------------------------------------------------------
    {
        //local
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
        {
            rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl() // ------------------------------------------------------------------------------------
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //reset current speed to cap
        if (flatVel.magnitude > capSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * capSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void GroundCheck() //---------------------------------------------------------------------------------------
    {
        RaycastHit hit;
        grounded = false;

        if (Physics.Raycast(orientation.position, Vector3.down, out hit, groundCheckDistance))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void Jump()  // ===/===/===MODULAR===/===/===MODULAR===/===/===MODULAR===/===/===MODULAR===/===/===MODULAR===/===/===MODULAR
    {
        if (grounded && Input.GetKey(KeyCode.Space) && canJump)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            StartCoroutine(JumpCooldown());
        }
    }

    private IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(0.15f);
        canJump = true;
    }

    private void Slide()  // ===/===/===MODULAR===/===/===MODULAR===/===/===MODULAR===/===/===MODULAR===/===/===MODULAR===/===/===MODULAR CALLED OFF
    {
        if (grounded && Input.GetKeyDown(KeyCode.LeftShift) && canSlide && !isSliding)
        {
            StartCoroutine(HandleSlide());
        }
    }

    private IEnumerator HandleSlide()
    {
        isSliding = true;
        canSlide = false;

        //change of both cap and actual move speed
        capSpeed = moveSpeed * slideSpeedMultiplier;
        moveSpeed = moveSpeed * slideSpeedMultiplier - 2f;
        playerCollider.height = originalColliderHeight * slideColliderHeightMultiplier;

        yield return new WaitForSeconds(slideDuration);

        //temp var
        capSpeed = originalCapSpeed;
        moveSpeed = originalMoveSpeed;
        playerCollider.height = originalColliderHeight;

        isSliding = false;

        StartCoroutine(SlideCooldown());
    }

    private IEnumerator SlideCooldown()
    {
        yield return new WaitForSeconds(slideCooldown);
        canSlide = true;
    }

    private void Crouch() // ===/===/===MODULAR===/===/===MODULAR===/===/===MODULAR===/===/===MODULAR===/===/===MODULAR===/===/===MODULAR  CALLED OFF
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isCrouching)
            {
                
                isCrouching = true;
                capSpeed *= crouchSpeedMultiplier; 
                playerCollider.height *= crouchColliderHeightMultiplier; 
            }
            else
            {
                
                isCrouching = false;
                capSpeed /= crouchSpeedMultiplier; 
                playerCollider.height = originalColliderHeight; 
            }
        }
    }



}
