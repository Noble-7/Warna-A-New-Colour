using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerBehaviour : MonoBehaviour
{

    public float maxSpeed = 10.0f;
    public float gravity = -30.0f;
    public float jumpHeight = 3.0f;
    public Vector3 velocity;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;
     
    public CharacterController controller;

    public Transform cam;

    public GameObject radialMenu;

    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference jumpControl;
    [SerializeField]
    private InputActionReference openRadialMenuControls;

    public AudioClip jumpAudio;
    public AudioClip landAudio;
    public AudioSource audioSource;

    private bool check = true;

    public ParticleSystem landDust;

    public int index = 0;

    private bool hasDoubleJumped = false;


    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
        openRadialMenuControls.action.Enable();
    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
        openRadialMenuControls.action.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        index = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
        }

        // movement

        float horizontal = movementControl.action.ReadValue<Vector2>().x;
        float vertical = movementControl.action.ReadValue<Vector2>().y;

        


        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * maxSpeed * Time.deltaTime);


            
        }

        // jumping
        if (jumpControl.action.triggered && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            audioSource.PlayOneShot(jumpAudio);

        }
        if (jumpControl.action.triggered && index == 2 && !hasDoubleJumped && !isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            audioSource.PlayOneShot(jumpAudio);
            hasDoubleJumped = true;
        }

        // gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if(isGrounded != check)
        {
            check = isGrounded;

            if(isGrounded == true)
            {
                audioSource.PlayOneShot(landAudio);
                StartCoroutine(PlayDust());
                hasDoubleJumped = false;
            }
        }

        if (index == 3)
        {
            maxSpeed = 15.0f;
        }
        else
        {
            maxSpeed = 10.0f;
        }

        if (openRadialMenuControls.action.triggered)
        {
            movementControl.action.Disable();
            jumpControl.action.Disable();
            radialMenu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0.1f;
            
        }

    }

    IEnumerator PlayDust()
    {
        landDust.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        landDust.gameObject.SetActive(false);


    }
}

