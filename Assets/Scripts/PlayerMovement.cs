using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour{
    [SerializeField]
    private float forceMagnitude = 850;

    [SerializeField]
    private float maxVelocity = 6;

    [SerializeField]
    private float rotationSpeed = 5;

    private Camera mainCamera;
    private Rigidbody body;
    private Vector3 movementDirection;


    private void Start(){
        body = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Update(){
        ProcessInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();
    }

    private void FixedUpdate(){
        if (movementDirection == Vector3.zero){
            return;
        }

        body.AddForce(movementDirection * (forceMagnitude * Time.deltaTime), ForceMode.Force);
        body.velocity = Vector3.ClampMagnitude(body.velocity, maxVelocity);
    }

    private void ProcessInput(){
        if (Touchscreen.current.primaryTouch.press.isPressed){
            var touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            var worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            movementDirection = transform.position - worldPosition;
            movementDirection.z = 0f;
            movementDirection.Normalize();
        }
        else{
            movementDirection = Vector3.zero;
        }
    }

    private void KeepPlayerOnScreen(){
        var newPosition = transform.position;
        var viewportPosition = mainCamera.WorldToViewportPoint(transform.position);
        newPosition.x = viewportPosition.x switch{
            > 1 => -newPosition.x + 0.1f,
            < 0 => -newPosition.x - 0.1f,
            _ => newPosition.x
        };

        newPosition.y = viewportPosition.y switch{
            > 1 => -newPosition.y + 0.1f,
            < 0 => -newPosition.y - 0.1f,
            _ => newPosition.y
        };

        transform.position = newPosition;
    }

    private void RotateToFaceVelocity(){
        if (body.velocity == Vector3.zero){
            return;
        }

        var targetRotation = Quaternion.LookRotation(body.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}