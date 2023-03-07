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
    private Rigidbody _rigidbody;
    private Vector3 _movementDirection;


    private void Start(){
        _rigidbody = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Update(){
        ProcessInput();
        KeepPlayerOnScreen();
        RotateToFaceVelocity();
    }

    private void FixedUpdate(){
        if (_movementDirection == Vector3.zero){
            return;
        }

        _rigidbody.AddForce(_movementDirection * (forceMagnitude * Time.deltaTime), ForceMode.Force);
        _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, maxVelocity);
    }

    private void ProcessInput(){
        if (Touchscreen.current.primaryTouch.press.isPressed){
            var touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            var worldPosition = mainCamera.ScreenToWorldPoint(touchPosition);
            _movementDirection = transform.position - worldPosition;
            _movementDirection.z = 0f;
            _movementDirection.Normalize();
        }
        else{
            _movementDirection = Vector3.zero;
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
        if (_rigidbody.velocity == Vector3.zero){
            return;
        }

        var targetRotation = Quaternion.LookRotation(_rigidbody.velocity, Vector3.back);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}