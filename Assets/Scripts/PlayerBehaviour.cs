//We stan for libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    //Warna's max health, used mostly for the healing powerup
    public int maxHealth = 100;
    //Keeps track of the current health. Why did I decide to comment this code.
    public int currentHealth;

    //We have a health bar script so, we need a ref to it. Enemies use this too. You can use this with anything so like destructible environment?
    public HealthBar healthBar;

    //I need this for the heal over time.
    private bool isHealing = false;

    //This is for tracking the sliding door in the tutorial level, but in theory we could use this for quests that need kills at one point. Get creative!
    public int kills = 0;


    //lava true variable
    private bool isOnLava = false;
    private bool isVeryOnLava = false;

    [Range(0, 100)]
    public int lavaDamage;

    [Range(0.0f, 60.0f)]
    public float lavaDamageTimeInSeconds;

    [Range(0, 100)]
    public int hotterLavaDamage;

    [Range(0.0f, 60.0f)]
    public float hotterLavaDamageTimeInSeconds;

    //Our max speed
    public float maxSpeed = 10.0f;
    //How quickly warna falls, this only affects her.
    public float gravity = -30.0f;
    //The height of the jump, we could probably make a powerup involving this
    public float jumpHeight = 3.0f;
    //Used to track movement with the character controller.
    public Vector3 velocity;
    //This is stuff for the camera? So that Warna turns smoothly towards the direction we're looking.
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //The location of the point that we use to verify if Warna is on the ground.
    public Transform groundCheck;
    //The radius of the sphere for the ground check. The bigger the number, the more generous the ground check.
    public float groundRadius = 0.5f;
    //The layer that interacts with the ground check.
    public LayerMask groundMask;
    //Bool used mostly for visual and audio effects.
    public bool isGrounded;

    //We need this for movement, so here it is
    public CharacterController controller;

    //This is needed to let the player know which direction is forward.
    public Transform cam;
    //The menu we use to change colours
    public GameObject radialMenu;
    //The actual pause menu, which since it has no code it sorta has to be a game object for now.
    public GameObject pauseMenu;
    //obligatory rigidbody moment
    public Rigidbody rb;
    //Needed for phase to work
    public Collider nose;

    //All our input objects, cause yay I guess
    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference jumpControl;
    [SerializeField]
    private InputActionReference openRadialMenuControls;
    [SerializeField]
    private InputActionReference abilityControls;
    [SerializeField]
    private InputActionReference cameraControls;
    [SerializeField]
    private InputActionReference pauseControls;

    //Anytime we have new audio we'll need it in here.
    public AudioClip jumpAudio;
    public AudioClip landAudio;
    public AudioClip healingAudio;
    public AudioClip phaseAudio;
    public AudioClip shootingAudio;
    public AudioClip damageAudio;
    public AudioSource audioSource;

    //This is for... Something, probably. (I checked, this is essentially a weird subset of isgrounded used for the landDust)
    private bool check = true;
    //This is for when we land, there's a bit of dust that trails off. 
    public ParticleSystem landDust;
    //Extremely important variable, keeps track of which colour we have equipped atm. Could probably turn this into an ENUM for readability.
    public int index = 0;

    private bool hasDoubleJumped = false;

    public Canvas dianaDialogue;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    public bool isGrappling = false;

    public bool hasBlue = false;
    public bool hasRed = false;
    public bool hasPurple = false;
    public bool hasGreen = false;

    public Animator wheelsAnimation;
    public Animator wheelsAnimation1;
    public Animator armsAnimation;
    public Animator armsAnimation1;
    public Animator idleAnimation;

    public bool hasKey;


    private void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
        openRadialMenuControls.action.Enable();
        abilityControls.action.Enable();
        cameraControls.action.Enable();
        pauseControls.action.Enable();

    }

    private void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
        openRadialMenuControls.action.Disable();
        abilityControls.action.Disable();
        cameraControls.action.Disable();
        pauseControls.action.Disable();

    }

    private void Awake()
    {
        if (PlayerPrefs.GetInt("usedContinue") != 0)
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat("xpos"), PlayerPrefs.GetFloat("ypos"), PlayerPrefs.GetFloat("zpos"));
            kills = PlayerPrefs.GetInt("kills");
            dianaDialogue.gameObject.SetActive(false);
            switch (PlayerPrefs.GetInt("hasRed"))
            {
                case 1:
                    hasRed = true;
                    break;
                case 0:
                    hasRed = false;
                    break;
            }

            switch (PlayerPrefs.GetInt("hasBlue"))
            {
                case 1:
                    hasBlue = true;
                    break;
                case 0:
                    hasBlue = false;
                    break;
            }

            switch (PlayerPrefs.GetInt("hasGreen"))
            {
                case 1:
                    hasGreen = true;
                    break;
                case 0:
                    hasGreen = false;
                    break;
            }

            switch (PlayerPrefs.GetInt("hasPurple"))
            {
                case 1:
                    hasPurple = true;
                    break;
                case 0:
                    hasPurple = false;
                    break;
            }

        }
        else
        {
            kills = 0;
            hasBlue = false;
            hasRed = false;
            hasGreen = false;
            hasPurple = false;
            PlayerPrefs.SetInt("hasBlue", 0);
            PlayerPrefs.SetInt("hasRed", 0);
            PlayerPrefs.SetInt("hasGreen", 0);
            PlayerPrefs.SetInt("hasPurple", 0);
            PlayerPrefs.SetInt("kills", 0);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        index = 0;

        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.activeInHierarchy)
        {
            if (dianaDialogue.isActiveAndEnabled || radialMenu.gameObject.activeInHierarchy)
            {
                movementControl.action.Disable();
                jumpControl.action.Disable();
                openRadialMenuControls.action.Disable();
                abilityControls.action.Disable();
                cameraControls.action.Disable();
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0.1f;
            }
            else
            {
                movementControl.action.Enable();
                jumpControl.action.Enable();
                openRadialMenuControls.action.Enable();
                abilityControls.action.Enable();
                cameraControls.action.Enable();
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }


            isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2.0f;
            }

            // movement

            float horizontal = movementControl.action.ReadValue<Vector2>().x;
            float vertical = movementControl.action.ReadValue<Vector2>().y;




            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            //look around
            if (direction.magnitude >= 0.1f && !isGrappling)
            {

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * maxSpeed * Time.deltaTime);
                wheelsAnimation.enabled = true;
                wheelsAnimation1.enabled = true;
                idleAnimation.SetInteger("AnimState", 1);


            }
            else
            {
                wheelsAnimation.enabled = false;
                wheelsAnimation1.enabled = false;
                idleAnimation.SetInteger("AnimState", 0);
            }

            //shootin
            if (index == 1)
            {
                
                if (abilityControls.action.triggered)
                {
                    GameObject instBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0, cam.rotation.eulerAngles.y, 0)) as GameObject;
                    audioSource.pitch = 1.0f;
                    audioSource.PlayOneShot(shootingAudio);
                    //Debug.Log(cam.rotation.y);
                    Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();
                    instBulletRigidBody.AddForce(new Vector3(cam.forward.x, 0, cam.forward.z) * bulletSpeed);
                    //Debug.Log(cam.forward);
                    Destroy(instBullet, 3.0f);
                }
            }

            // jumping
            if (jumpControl.action.triggered && isGrounded && !isGrappling)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
                audioSource.pitch = (Random.Range(0.8f, 0.9f));
                audioSource.volume = 0.3f;
                audioSource.PlayOneShot(jumpAudio);
                StartCoroutine(JumpAnim());
                //audioSource.pitch = 1.0f;

            }
            if (jumpControl.action.triggered && index == 2 && !hasDoubleJumped && !isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
                audioSource.pitch = (Random.Range(1.1f, 1.2f));
                audioSource.volume = 0.3f;
                audioSource.PlayOneShot(jumpAudio);
                //audioSource.pitch = 1.0f;
                hasDoubleJumped = true;
                StartCoroutine(JumpAnim());
            }

            if (!isGrappling)
            {
                // gravity
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }

            if (isGrounded != check)
            {
                check = isGrounded;

                if (isGrounded == true)
                {
                    
                    //audioSource.PlayOneShot(landAudio);
                    StartCoroutine(PlayDust());
                    hasDoubleJumped = false;
                    StopCoroutine(JumpAnim());
                }
            }

            //not this for particles
            if (index == 3)
            {
                //Leftovers from a simpler time
                //maxSpeed = 15.0f;
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"), LayerMask.NameToLayer("Wall"), true);
                //Debug.Log("Ignoring layers");
            }
            else
            {
                //Leftovers from a simpler time
                //maxSpeed = 10.0f;
                Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Bullet"), LayerMask.NameToLayer("Wall"), false);
                //Debug.Log("No longer ignoring layers");
            }

            if (index == 4 && abilityControls.action.triggered && currentHealth < maxHealth && !isHealing)
            {
                StartCoroutine(Heal());

            }
            if (index != 4)
            {
                StopCoroutine(Heal());
            }



        }
        if (pauseControls.action.triggered)
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            if (pauseMenu.activeInHierarchy)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                Time.timeScale = 0.0f;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1.0f;
            }
        }

        

    }

    void OnCollisionEnter(Collision other)
    {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("Phase"))
        {
           
            if (index == 3)
            {
                other.gameObject.GetComponent<MeshCollider>().convex = true;
                other.gameObject.GetComponent<Collider>().isTrigger = true;
                Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), rb.GetComponent<Collider>(), true);
                Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), nose, true);

                audioSource.PlayOneShot(phaseAudio);
            }
        }
    }

    //void OnCollisionExit(Collision other)
    //{
       
    //    if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
    //    {
           
    //        Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), rb.GetComponent<Collider>(), false);
    //        Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), nose, false);
            
           
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);

        }
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(10);
            Destroy(other);
            audioSource.PlayOneShot(damageAudio);
        }
        if (other.gameObject.CompareTag("Lava"))
        {
            isOnLava = true;
            StartCoroutine(LavaDamage());
        }
        if (other.gameObject.CompareTag("HotterLava"))
        {
            isVeryOnLava = true;
            StartCoroutine(HotterLavaDamage());
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("OnTriggerExit");
        if (other.gameObject.CompareTag("Lava"))
        {
            isOnLava = false;

            StopCoroutine(LavaDamage());
        }
        if (other.gameObject.CompareTag("HotterLava"))
        {
            isVeryOnLava = false; 
            StopCoroutine(HotterLavaDamage());
        }
        if(other.gameObject.layer == LayerMask.NameToLayer("Phase"))
        {
            //Debug.Log("Exited wall");
            other.gameObject.GetComponent<Collider>().isTrigger = false;
            other.gameObject.GetComponent<MeshCollider>().convex = false;
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), rb.GetComponent<Collider>(), false);
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), nose, false);
            //Debug.Log(Physics.GetIgnoreCollision(other.gameObject.GetComponent<Collider>(), rb.GetComponent<Collider>()));
            //Debug.Log(Physics.GetIgnoreCollision(other.gameObject.GetComponent<Collider>(), nose));
        }
    }

    IEnumerator PlayDust()
    {
        landDust.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        landDust.gameObject.SetActive(false);


    }

    IEnumerator LavaDamage()
    {
        while (isOnLava == true)
        {            
            TakeDamage(lavaDamage);
            healthBar.setHealth(currentHealth);
            yield return new WaitForSeconds(lavaDamageTimeInSeconds);
        }
    }

    IEnumerator HotterLavaDamage()
    {
        while (isVeryOnLava == true)
        {            
            TakeDamage(hotterLavaDamage);
            healthBar.setHealth(currentHealth);
            yield return new WaitForSeconds(hotterLavaDamageTimeInSeconds);
        }
    }

    IEnumerator Heal()
    {
        //Debug.Log("Begun healing");
        audioSource.pitch = 1.0f;
        audioSource.volume = 0.6f;
        audioSource.PlayOneShot(healingAudio);
        while (currentHealth < maxHealth)
        {
            //Debug.Log("healing");
            isHealing = true;

            //healing value
            currentHealth += 2;
            healthBar.setHealth(currentHealth);
            yield return new WaitForSeconds(0.1f);
        }
        //Debug.Log("healed");
        isHealing = false;
    }

    IEnumerator JumpAnim()
    {
        armsAnimation.enabled = true;
        armsAnimation1.enabled = true;
        yield return new WaitForSeconds(1.5f);
        armsAnimation.enabled = false;
        armsAnimation1.enabled = false;
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
        if (currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            SceneManager.LoadScene("Loss Screen");
        }
    }




}

