using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Player_motor : MonoBehaviour {
    [SerializeField]
    private Camera cam;


    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;
    private Vector3 thrusterForce = Vector3.zero;

    [SerializeField]
    private float cameraRotationlimit = 85f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Gets a movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Gets a rotation vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Gets a rotation vector for the camera
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    //Get a force vector for our thrusters
    public void Applythruster(Vector3 _thrusterForce)
    {
        thrusterForce = _thrusterForce;
    }

    //Run every physics iteration
    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    //Perform movement based on velocity variable 
    void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if(thrusterForce!=Vector3.zero)
        {
            rb.AddForce(thrusterForce * Time.fixedDeltaTime, ForceMode.Acceleration);
            
        }
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        //Set our roattion and clamp it
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationlimit, cameraRotationlimit);
        //Apply our rotation to the transform of our camera
        cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

}
