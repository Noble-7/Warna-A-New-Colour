using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class RadialMenuController : MonoBehaviour
{
    public GameObject theMenu;

    public PlayerBehaviour player;
    public Swinging swinging;
    public MeshRenderer playerColour;
    public Material Grey, Red, Blue, Green, Purple;

    public Vector2 moveInput;

    public Text[] options;

    public Color normalColor, highlightedColor, notCollectedColour;

    public int selectedOption;

    public GameObject highlightBlock;

    [SerializeField]
    private InputActionReference openRadialMenuControls;
    [SerializeField]
    private InputActionReference mousePositionControl;
    [SerializeField]
    private InputActionReference selectControl;
    [SerializeField]
    private InputActionReference movementControl;
    [SerializeField]
    private InputActionReference jumpControl;


    private void OnEnable()
    {
        openRadialMenuControls.action.Enable();
        mousePositionControl.action.Enable();
        selectControl.action.Enable();
    }

    private void OnDisable()
    {
        openRadialMenuControls.action.Disable();
        mousePositionControl.action.Disable();
        selectControl.action.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Radial Menu Start");
    }

    // Update is called once per frame
    void Update()
    {
        if(openRadialMenuControls.action.triggered)
        {
            theMenu.SetActive(true);
        }

        if(theMenu.activeInHierarchy == true)
        {
            moveInput.x = mousePositionControl.action.ReadValue<Vector2>().x - (Screen.width / 2.0f);
            moveInput.y = mousePositionControl.action.ReadValue<Vector2>().y - (Screen.height / 2.0f);
            moveInput.Normalize();

            //Debug.Log(moveInput);

            if(moveInput != Vector2.zero)
            {
                float angle = Mathf.Atan2(moveInput.y, -moveInput.x) / Mathf.PI;
                angle *= 180;
                angle += 90.0f;
                if(angle < 0)
                {
                    angle += 360;
                }

                for (int i = 0; i < options.Length; i++)
                {
                    if(angle > i * (360/options.Length) && angle < (i + 1) * (360/options.Length))
                    {
                        //Debug.Log("Segment: " + i);
                        options[i].color = highlightedColor;
                        selectedOption = i;

                        highlightBlock.transform.rotation = Quaternion.Euler(0, 0, i * -(360 / options.Length));
                    }
                    else
                    {
                        options[i].color = normalColor;
                    }
                }
            }

            if (selectControl.action.triggered)
            {
                switch (selectedOption)
                {
                    case 0:
                        //Run purple code
                        if (player.hasPurple)
                        {
                            player.index = 3;
                            playerColour.material = Purple;
                        }
                        break;
                    case 1:
                        //Run red code
                        if (player.hasRed)
                        {
                            player.index = 1;
                            playerColour.material = Red;
                        }
                        break;
                    case 2:
                        //Run grey code

                        player.index = 0;
                        playerColour.material = Grey;
                        break;
                    case 3:
                        //Run green code
                        if (player.hasGreen)
                        {
                            player.index = 4;
                            playerColour.material = Green;
                        }
                        break;
                    case 4:
                        //Run blue code
                        if (player.hasBlue)
                        {
                            player.index = 2;
                            playerColour.material = Blue;
                        }
                        break;
                }

                theMenu.SetActive(false);
            }
        }

        //if(Input.GetKeyDown(KeyCode.Q))
        //{
        //    SwitchRed();
        //}

        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    SwitchBlue();
        //}

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    SwitchGreen();
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    SwitchYellow();
        //}

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    SwitchAll();
        //}
    }

    
}
