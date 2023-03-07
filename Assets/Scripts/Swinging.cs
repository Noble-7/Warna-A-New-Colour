//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityEngine.UI;
//using TMPro;

//public class Swinging : MonoBehaviour
//{
//    private LineRenderer lr;
//    private Vector3 grapplePoint;
//    public LayerMask whatIsGrappleable;
//    public Transform gunTip, camera, player;
//    public float maxDistance = 50f;
//    private SpringJoint joint;
//    public GameObject grapplinGun;
//    bool isHeld = false;


//    public PlayerBehaviour playerRef;
//    public Transform orientation;
//    [SerializeField]
//    private InputActionReference movementControl;
//    [SerializeField]
//    private InputActionReference boostControl;
//    [SerializeField]
//    private InputActionReference swingingControls;

//    private void OnEnable()
//    {

//    }
//    private void OnDisable()
//    {

//    }


//    void Awake()
//    {
//        lr = GetComponentInChildren<LineRenderer>();
//        //grapplinGun = GetComponent<GameObject>();
//        //playerRef = GetComponent<PlayerBehaviour>();
//    }

//    void Update()
//    {
//        //Debug.Log(playerRef.index);

//        if (playerRef.index == 4 && isHeld == true)
//        {
//            OdmGearMovement();
//        }
//        else if (playerRef.index != 4 || isHeld == false)
//        {
//            StopGrapple();
//        }

//        if (playerRef.index == 4)
//        {

//            grapplinGun.SetActive(true);
//        }
//        else
//        {
//            grapplinGun.SetActive(false);
//        }

//        if (swingingControls.action.triggered && playerRef.index == 4)
//        {
//            StartGrapple();
//            isHeld = true;
//        }
//        else if (swingingControls.action.WasReleasedThisFrame())
//        {
//            isHeld = false;
//        }
//    }

//    //Called after Update
//    void LateUpdate()
//    {
//        DrawRope();
//    }

//    /// <summary>
//    /// Call whenever we want to start a grapple
//    /// </summary>
//    void StartGrapple()
//    {
//        RaycastHit hit;
//        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
//        {
//            playerRef.isGrappling = true;
//            grapplePoint = hit.point;
//            joint = player.gameObject.AddComponent<SpringJoint>();
//            joint.autoConfigureConnectedAnchor = false;
//            joint.connectedAnchor = grapplePoint;

//            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

//            //The distance grapple will try to keep from grapple point. 
//            joint.maxDistance = distanceFromPoint * 0.8f;
//            joint.minDistance = distanceFromPoint * 0.25f;

//            //Adjust these values to fit your game.
//            joint.spring = 4.5f;
//            joint.damper = 7f;
//            joint.massScale = 4.5f;

//            lr.positionCount = 2;
//            currentGrapplePosition = gunTip.position;

//            playerRef.controller.enabled = false;

//        }
//    }


//    /// <summary>
//    /// Call whenever we want to stop a grapple
//    /// </summary>
//    void StopGrapple()
//    {
//        playerRef.isGrappling = false;
//        lr.positionCount = 0;
//        Destroy(joint);

//        playerRef.controller.enabled = true;
//    }

//    private Vector3 currentGrapplePosition;

//    void DrawRope()
//    {
//        //If not grappling, don't draw rope
//        if (!joint) return;

//        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

//        lr.SetPosition(0, gunTip.position);
//        lr.SetPosition(1, currentGrapplePosition);
//    }

//    public bool IsGrappling()
//    {
//        return joint != null;
//    }

//    public Vector3 GetGrapplePoint()
//    {
//        return grapplePoint;
//    }

//    private void OdmGearMovement()
//    {
//        // right
//        if (movementControl.action.ReadValue<Vector2>().x > 0.1)
//        {
//            playerRef.rb.AddForce(orientation.right * 2000.0f * Time.deltaTime);
//            Debug.Log("yeehawright");
//        }

//        // left
//        if (movementControl.action.ReadValue<Vector2>().x < 0.1)
//        {
//            playerRef.rb.AddForce(-orientation.right * 2000.0f * Time.deltaTime);
//            Debug.Log("yeehawleft");
//        }

//        // forward
//        if (movementControl.action.ReadValue<Vector2>().y > 0.1)
//        {
//            playerRef.rb.AddForce(orientation.forward * 2000.0f * Time.deltaTime);
//            Debug.Log("yeehawforward");
//        }

//        // shorten cable/boost to point
//        if (boostControl.action.triggered)
//        {
//            Vector3 directionToPoint = grapplePoint - transform.position;
//            playerRef.rb.AddForce(directionToPoint.normalized * 2000.0f * Time.deltaTime, ForceMode.Acceleration);

//            float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint);

//            joint.maxDistance = distanceFromPoint * 0.8f;
//            joint.minDistance = distanceFromPoint * 0.25f;
//            Debug.Log("yeehawjumpfool");

//        }
//        // extend cable
//        if (movementControl.action.ReadValue<Vector2>().y < 0.1)
//        {
//            float extendedDistanceFromPoint = Vector3.Distance(transform.position, grapplePoint) + 20.0f;

//            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
//            joint.minDistance = extendedDistanceFromPoint * 0.25f;
//            Debug.Log("yeehawback");
//        }
//    }

//}
