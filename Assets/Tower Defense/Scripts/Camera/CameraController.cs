using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Core.Patterns;
public class CameraController : Singleton<CameraController>
{
    [Required]
    [SceneObjectsOnly]
    public Transform cameraTransform;

    [PropertyRange(0f, 1f)]
    public float normalSpeed = 0.25f;
    [PropertyRange(1f, 3f)]
    public float fastSpeed = 1f;
    [PropertyRange(0.15f, 3f)]
    public float movementSpeed = 1f;
    [PropertyRange(1f, 5f)]
    public float movementTime = 5f;
    [PropertyRange(0.5f, 2f)]
    public float rotationAmout = 1f;
    [PropertyRange(1f, 10f)]
    public float zoomAmout = 1f;

    /// 
    /// Internal parameters
    /// 
    [HideInEditorMode]
    public Transform followTransform;
    [HideInEditorMode]
    public Vector3 newPosition;
    [HideInEditorMode]
    public Quaternion newRotation;
    [HideInEditorMode]
    public Vector3 dragStartPosition;
    [HideInEditorMode]
    public Vector3 dragCurrentPosition;
    [HideInEditorMode]
    public Vector3 rotateStartPosition;
    [HideInEditorMode]
    public Vector3 rotateCurrentPosition;

    void Start()
    {
        newPosition = transform.position;
        newRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (!PlayerManager.instance.IsLockingPlayerControl)
        {
            HandleCameraTargetLook();

            HandleSpeedInput();
            HandleHorizontalMovementInput();
            HandleVerticalMovementInput();
            HandleRotationInput();

            HandleMovement();
            HandleRotation();
        }
    }

    // Methods [START]
    void HandleCameraTargetLook()
    {
        cameraTransform.LookAt(this.transform);
    }

    //Handles camera zooming
    void HandleVerticalMovementInput()
    {
        Vector3 direction = cameraTransform.position - this.transform.position;
        direction.Normalize();
        direction *= zoomAmout;

        //Mouse
        if (Input.mouseScrollDelta.y != 0)
        {
            newPosition += Input.mouseScrollDelta.y * -direction;
        }

        //Keyboard
        if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus))
        {
            newPosition -= direction * 0.15f;
        }
        if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus))
        {
            newPosition += direction * 0.15f;
        }

        //Camera Bounds clamping
        NewPositionClamping();
    }
    void HandleRotationInput()
    {
        //Mouse
        if (Input.GetMouseButtonDown(2))
        {
            rotateStartPosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            rotateCurrentPosition = Input.mousePosition;

            Vector3 difference = rotateStartPosition - rotateCurrentPosition;

            rotateStartPosition = rotateCurrentPosition;

            newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
        }

        //Keyboard
        if (Input.GetKey(KeyCode.Q))
        {
            newRotation *= Quaternion.Euler(Vector3.up * -rotationAmout);
        }
        if (Input.GetKey(KeyCode.E))
        {
            newRotation *= Quaternion.Euler(Vector3.up * rotationAmout);
        }
    }

    void HandleSpeedInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = fastSpeed;
        }
        else
        {
            movementSpeed = normalSpeed;
        }
    }

    void HandleHorizontalMovementInput()
    {
        //Mouse Movement
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);

                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }

        //Keyboard Movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.forward * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.forward * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }

        //Reset Camera Follow
        if (
            Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)
            || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)
            || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)
            || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)
            || Input.GetMouseButtonUp(0) || Input.GetKeyDown(KeyCode.Escape)
            )
        {
            followTransform = null;
        }

        //Camera follow
        if (followTransform != null)
            newPosition = followTransform.position;

        //Camera Bounds clamping
        NewPositionClamping();
    }

    void HandleMovement()
    {
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);
    }
    void HandleRotation()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * movementTime);
    }
    // Methods [END]


    // Secondary Methods [START]
    void NewPositionClamping()
    {
        float distance = Vector3.Distance(newPosition, CameraBounds.instance.Center);

        if (distance > CameraBounds.instance.Radius)
        {
            Vector3 fromOriginToObject = newPosition - CameraBounds.instance.Center; //~GreenPosition~ - *BlackCenter*
            fromOriginToObject *= CameraBounds.instance.Radius / distance; //Multiply by radius //Divide by Distance
            newPosition = CameraBounds.instance.Center + fromOriginToObject; //*BlackCenter* + all that Math
        }

        newPosition = new Vector3(
                newPosition.x,
                Mathf.Clamp(newPosition.y, CameraBounds.instance.Center.y - (CameraBounds.instance.Height / 2), CameraBounds.instance.Center.y + (CameraBounds.instance.Height / 2)),
                newPosition.z
            );
    }
    // Secondary Methods [END]
}

////////////////////////////////////////////////////////////////////////////////
////////////SCRIPT MADE BY JAISON ROBSON GUSAVA UNDER MIT LICENSE///////////////
/////////////////////// https://github.com/jaisonrobson/ ///////////////////////