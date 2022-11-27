using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class Swinging : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;
    public GameObject grapplinGun;

    public PlayerBehaviour playerRef;
    public Transform orientation;
    public Rigidbody rb;

    [SerializeField]
    private InputActionReference swingingControls;

    void Awake()
    {
        lr = GetComponentInChildren<LineRenderer>();
        //grapplinGun = GetComponent<GameObject>();
        //playerRef = GetComponent<PlayerBehaviour>();
    }

    void Update()
    {
        Debug.Log(playerRef.index);

        if (playerRef.index == 4 && swingingControls.action.triggered)
        {
            StartGrapple();
        }
        else if (playerRef.index != 4)
        {
            StopGrapple();
        }

        if (playerRef.index == 4)
        {

            grapplinGun.SetActive(true);
        }
        else
        {
            grapplinGun.SetActive(false);
        }
    }

    //Called after Update
    void LateUpdate()
    {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            playerRef.isGrappling = true;
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple()
    {
        playerRef.isGrappling = false;
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    private void OdmGearMovement()
    {
        // right
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(orientation.right * 2000.0f * Time.deltaTime);

        }

        // left
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-orientation.right * 2000.0f * Time.deltaTime);

        }

        // forward
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(orientation.forward * 2000.0f * Time.deltaTime);
        }

        // shorten cable/boost to point
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = grapplePoint - transform.position;
            rb.AddForce(directionToPoint.normalized * 2000.0f * Time.deltaTime, ForceMode.Acceleration);

            float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

        }
        // extend cable
        if (Input.GetKey(KeyCode.S))
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, grapplePoint) + 20.0f;

            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }
    }
}

