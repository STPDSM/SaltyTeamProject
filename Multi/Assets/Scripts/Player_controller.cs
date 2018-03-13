using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(Player_motor))]

public class Player_controller : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float LookSensitivity = 3f;

    [SerializeField]
    private float thrusterForce = 1000f;

    [Header("Spring settings")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 40f;

    private Player_motor motor;
    private ConfigurableJoint joint;

    private void Start()
    {
        motor = GetComponent<Player_motor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
    }

    private void Update()
    {
        //Calculate movement velocity
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");


        Vector3 _movHorizon = transform.right * _xMov;
        Vector3 _movVerti = transform.forward * _zMov;

        //Final movement vector
        Vector3 _velocity = (_movHorizon + _movVerti).normalized * speed;

        //Apply movement
        motor.Move(_velocity);

        //Calculate rotation as a 3D vector: turning player around
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * LookSensitivity;

        //Apply rotation
        motor.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector: turning player around
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * LookSensitivity;

        //Apply camera rotation
        motor.RotateCamera(_cameraRotationX);

        //Calculate the thrusterforce based on player input
        Vector3 _thrusterForce = Vector3.zero;
        if(Input.GetButton("Jump"))
        {
            _thrusterForce = Vector3.up * thrusterForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }

        //Apply the thusterforce
        motor.Applythruster(_thrusterForce);

    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive { mode = jointMode,
            positionSpring = jointSpring,
            maximumForce = jointMaxForce };
    }

}
