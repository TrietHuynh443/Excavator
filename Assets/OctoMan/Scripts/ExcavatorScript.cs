using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ExcavatorScript : MonoBehaviour
{
    [SerializeField][Range(0f, 1f)] private float _shovelControlSpeed = 0.5f;
    [SerializeField][Range(0f, 1f)] private float _bigArmControlSpeed = 0.5f;
    [SerializeField][Range(0f, 1f)] private float _smallArmControlSpeed = 0.5f;
    [SerializeField][Range(0f, 1f)] private float _rotateBodyControlSpeed = 0.6f;
    [SerializeField] private float rotSpeed = 10f;
    [SerializeField] private float _driveSpeed = 15f;
    [SerializeField] private SpeedBarController _speedController;
    [SerializeField] private float _speedPerBatch = 5f;

    private float _maxSpeed;

    private float _minSpeed;
    //Animator
    public Animator anim;


    //Door
    bool opened = false;

    public bool InDriveMode = true;
    //Animate UV'S
    public float scrollSpeed = 0.5f;

    float offsetL;
    float offsetR;

    public bool U = false;
    public bool V = true;

    private Material matL;
    private Material matR;

    //Treads
    public GameObject TreadsL;
    public GameObject TreadsR;

    //Weight Points - determines the rotation and movement axis of the Excavator
    public GameObject leftTread;
    public GameObject rightTread;

    //Big Wheels
    public GameObject WheelFrontLeft;
    public GameObject WheelFrontRight;

    public GameObject WheelBackLeft;
    public GameObject WheelBackRight;


    private Rigidbody _rigidbody;

    private int _maxSpeedBatch;
    void Start()
    {
        // Materials for the Treads
        matL = TreadsL.GetComponent<Renderer>().material;
        matR = TreadsR.GetComponent<Renderer>().material;

        //set the bigarm to a non colliding position
        anim.SetFloat("BigArmSpeed", 10f);
        anim.Play("BigArmPosition", 1, (1 / 30) * 5f);

        _treadPositions[0] = leftTread.transform.position;
        _treadPositions[1] = rightTread.transform.position;

        _wheelsFront[0] = WheelFrontLeft;
        _wheelsFront[1] = WheelFrontRight;

        _wheelsBack[0] = WheelBackLeft;
        _wheelsBack[1] = WheelBackRight;

        _rigidbody = GetComponent<Rigidbody>();
        _maxSpeedBatch = _speedController.MaxSpeedLevel;
        _maxSpeed = _driveSpeed + (_maxSpeedBatch - 1) * _speedPerBatch;
        _minSpeed = _driveSpeed;
    }

    //void Update()
    //{
    //    //-------------------------------------------------BIG ARM-----------------------------------------------------------------
    //    if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.X) && anim.GetInteger("BigArmPosition") != 2)
    //    {
    //        anim.SetInteger("BigArmPosition", 1);
    //        anim.SetFloat("BigArmSpeed", 1f);
    //    }
    //    else if (!Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.X) && anim.GetInteger("BigArmPosition") != 0)
    //    {
    //        anim.SetInteger("BigArmPosition", 1);
    //        anim.SetFloat("BigArmSpeed", -1f);
    //    }
    //    else
    //    {
    //        anim.SetFloat("BigArmSpeed", 0);
    //    }

    //    //-------------------------------------------------------SMALL ARM-------------------------------------------------------------
    //    if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && anim.GetInteger("SmallArmPosition") != 2)
    //    {
    //        anim.SetInteger("SmallArmPosition", 1);
    //        anim.SetFloat("SmallArmSpeed", 1f);
    //    }
    //    else if (!Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow) && anim.GetInteger("SmallArmPosition") != 0)
    //    {
    //        anim.SetInteger("SmallArmPosition", 1);
    //        anim.SetFloat("SmallArmSpeed", -1f);
    //    }
    //    else
    //    {
    //        anim.SetFloat("SmallArmSpeed", 0);
    //    }

    //    //----------------------------------------------------------SHOVEL-----------------------------------------------------------------
    //    if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && anim.GetInteger("ShovelPosition") != 2)
    //    {
    //        anim.SetInteger("ShovelPosition", 1);
    //        anim.SetFloat("ShovelSpeed", 1f);
    //    }
    //    else if (!Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) && anim.GetInteger("ShovelPosition") != 0)
    //    {
    //        anim.SetInteger("ShovelPosition", 1);
    //        anim.SetFloat("ShovelSpeed", -1f);
    //    }
    //    else
    //    {
    //        anim.SetFloat("ShovelSpeed", 0);
    //    }

    //    //---------------------------------------------------------ROTATE BODY----------------------------------------------------------
    //    if (Input.GetKey(KeyCode.Z) && !Input.GetKey(KeyCode.C))
    //    {
    //        anim.SetFloat("RotateSpeed", 0.5f);
    //    }
    //    else if (!Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.C))
    //    {
    //        anim.SetFloat("RotateSpeed", -0.5f);
    //    }
    //    else
    //    {
    //        anim.SetFloat("RotateSpeed", 0f);
    //    }


    //    //---------------------------------------------------------DRIVE MODE--------------------------------------------------------------

    //    //ANIMATE RIGHT TREAD
    //    if (Input.GetKey(KeyCode.Y) && !Input.GetKey(KeyCode.H))
    //    {
    //        transform.RotateAround(leftTread.transform.position, -Vector3.up, Time.deltaTime * rotSpeed);
    //        offsetR = Time.time * scrollSpeed % 1;
    //        WheelFrontRight.transform.Rotate(Vector3.forward * Time.deltaTime * rotSpeed * 4);
    //        WheelBackRight.transform.Rotate(Vector3.forward * Time.deltaTime * rotSpeed * 4);

    //    }

    //    if (!Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.H))
    //    {
    //        transform.RotateAround(leftTread.transform.position, Vector3.up, Time.deltaTime * rotSpeed);
    //        offsetR = Time.time * -scrollSpeed % 1;
    //        WheelFrontRight.transform.Rotate(-Vector3.forward * Time.deltaTime * rotSpeed * 4);
    //        WheelBackRight.transform.Rotate(-Vector3.forward * Time.deltaTime * rotSpeed * 4);
    //    }

    //    //ANIMATE LEFT TREAD
    //    if (Input.GetKey(KeyCode.T) && !Input.GetKey(KeyCode.G))
    //    {
    //        transform.RotateAround(rightTread.transform.position, Vector3.up, Time.deltaTime * rotSpeed);
    //        offsetL = Time.time * scrollSpeed % 1;
    //        WheelFrontLeft.transform.Rotate(-Vector3.forward * Time.deltaTime * rotSpeed * 4);
    //        WheelBackLeft.transform.Rotate(-Vector3.forward * Time.deltaTime * rotSpeed * 4);
    //    }

    //    if (!Input.GetKey(KeyCode.T) && Input.GetKey(KeyCode.G))
    //    {
    //        transform.RotateAround(rightTread.transform.position, -Vector3.up, Time.deltaTime * rotSpeed);
    //        offsetL = Time.time * -scrollSpeed % 1;
    //        WheelFrontLeft.transform.Rotate(Vector3.forward * Time.deltaTime * rotSpeed * 4);
    //        WheelBackLeft.transform.Rotate(Vector3.forward * Time.deltaTime * rotSpeed * 4);
    //    }


    //    //------------------------------------------------------DOOR OPEN / CLOSE-----------------------------------------------------
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        opened = !opened;
    //        anim.SetBool("DoorOpen", opened);
    //    }


    //    //--------------------------------------------------------------Animate UV's---------------------------------------------------
    //    if (U && V)
    //    {
    //        matL.mainTextureOffset = new Vector2(offsetL, offsetL);
    //        matR.mainTextureOffset = new Vector2(offsetR, offsetR);
    //    }
    //    else if (U)
    //    {
    //        matL.mainTextureOffset = new Vector2(offsetL, 0);
    //        matR.mainTextureOffset = new Vector2(offsetR, 0);
    //    }
    //    else if (V)
    //    {
    //        matL.mainTextureOffset = new Vector2(0, offsetL);
    //        matR.mainTextureOffset = new Vector2(0, offsetR);
    //    }
    //}
    void Update()
    {
        ControlSafetyBlockLever();
        if (_isSafetyBlockLeverEngage)
        {
            ControlSpeed();
        }

        ControlDoor();
        //--------------------------------------------------------------Animate UV's---------------------------------------------------
    }

    private void ControlSpeed()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _speedController.IncreaseSpeed();
            StopAllCoroutines();
            StartCoroutine(ChangeSpeed(Mathf.Min(_maxSpeed, _driveSpeed + _speedPerBatch)));

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _speedController.DecreaseSpeed();
            StopAllCoroutines();
            StartCoroutine(ChangeSpeed(Mathf.Max(_minSpeed, _driveSpeed - _speedPerBatch)));
        }
    }

    private IEnumerator ChangeSpeed(float newSpeed)
    {
        Debug.Log("new speed " + newSpeed);
        float elapsedTime = 0f;
        while (elapsedTime < _speedUpDuration)
        {
            elapsedTime += Time.deltaTime;
            float speed = Mathf.Lerp(_driveSpeed, newSpeed, elapsedTime / _speedUpDuration);
            _driveSpeed = speed;
            yield return null;
        }

    }

    private void StopControling()
    {
        anim.SetFloat("RotateSpeed", 0f);
        anim.SetFloat("ShovelSpeed", 0);
        anim.SetFloat("BigArmSpeed", 0);
    }

    private void MovingControl()
    {
        Drive();
        if (U && V)
        {
            matL.mainTextureOffset = new Vector2(offsetL, offsetL);
            matR.mainTextureOffset = new Vector2(offsetR, offsetR);
        }
        else if (U)
        {
            matL.mainTextureOffset = new Vector2(offsetL, 0);
            matR.mainTextureOffset = new Vector2(offsetR, 0);
        }
        else if (V)
        {
            matL.mainTextureOffset = new Vector2(0, offsetL);
            matR.mainTextureOffset = new Vector2(0, offsetR);
        }
    }

    private void FixedUpdate()
    {
        if (!_isSafetyBlockLeverEngage)
        {
            ////-------------------------------------------------BIG ARM-----------------------------------------------------------------
            MoveBoom();
            ////-------------------------------------------------------SMALL ARM-------------------------------------------------------------
            MoveArm();
            ////----------------------------------------------------------SHOVEL-----------------------------------------------------------------
            ControlShovel();
            ////---------------------------------------------------------ROTATE BODY----------------------------------------------------------
            RotateBody();
            ////------------------------------------------------------DOOR OPEN / CLOSE-----------------------------------------------------
        }
        else
        {
            MovingControl();
        }
    }
    private void ControlSafetyBlockLever()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isSafetyBlockLeverEngage = !_isSafetyBlockLeverEngage;
            StopControling();
        }
    }

    private void ControlDoor()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            opened = !opened;
            anim.SetBool("DoorOpen", opened);
        }
    }
    enum Tread
    {
        LEFT = 0,
        RIGHT = 1,
    }

    enum Clockwise
    {
        COUNTER_CLOCKWISE = -1,
        CLOCKWISE = 1,
    }
    private void Drive()
    {

        _treadPositions[0] = leftTread.transform.position;
        _treadPositions[1] = rightTread.transform.position;

        //ANIMATE RIGHT TREAD
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            RotateTread(Tread.RIGHT, Clockwise.COUNTER_CLOCKWISE, rotSpeed);
            offsetR = Time.time * scrollSpeed % 1;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            RotateTread(Tread.RIGHT, Clockwise.CLOCKWISE, rotSpeed);
            //transform.RotateAround(leftTread.transform.position, Vector3.up, Time.deltaTime * rotSpeed);
            offsetR = Time.time * -scrollSpeed % 1;
            //WheelFrontRight.transform.Rotate(-Vector3.forward * Time.deltaTime * rotSpeed * 4);
            //WheelBackRight.transform.Rotate(-Vector3.forward * Time.deltaTime * rotSpeed * 4);
        }

        //ANIMATE LEFT TREAD
        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
        {
            RotateTread(Tread.LEFT, Clockwise.COUNTER_CLOCKWISE, rotSpeed);
            //transform.RotateAround(rightTread.transform.position, Vector3.up, Time.deltaTime * rotSpeed);
            offsetL = Time.time * -scrollSpeed % 1;
            //WheelFrontLeft.transform.Rotate(-Vector3.forward * Time.deltaTime * rotSpeed * 4);
            //WheelBackLeft.transform.Rotate(-Vector3.forward * Time.deltaTime * rotSpeed * 4);
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
        {
            RotateTread(Tread.LEFT, Clockwise.CLOCKWISE, rotSpeed);
            //transform.RotateAround(rightTread.transform.position, -Vector3.up, Time.deltaTime * rotSpeed);
            offsetL = Time.time * scrollSpeed % 1;
            //WheelFrontLeft.transform.Rotate(Vector3.forward * Time.deltaTime * rotSpeed * 4);
            //WheelBackLeft.transform.Rotate(Vector3.forward * Time.deltaTime * rotSpeed * 4);
        }

        else
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //RotateTread(Tread.RIGHT, Clockwise.CLOCKWISE, _driveSpeed);
                offsetR = Time.time * -scrollSpeed % 1;
                //RotateTread(Tread.LEFT, Clockwise.COUNTER_CLOCKWISE, _driveSpeed);
                offsetL = Time.time * -scrollSpeed % 1;
                _rigidbody.velocity = _driveSpeed  * Vector3.forward;
                _rigidbody.angularVelocity = Vector3.zero;

            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                //RotateTread(Tread.RIGHT, Clockwise.COUNTER_CLOCKWISE, _driveSpeed);
                offsetR = Time.time * scrollSpeed % 1;

                //RotateTread(Tread.LEFT, Clockwise.CLOCKWISE, _driveSpeed);
                offsetL = Time.time * scrollSpeed % 1;
                _rigidbody.velocity = _driveSpeed * -Vector3.forward;

                _rigidbody.angularVelocity = Vector3.zero;
            }
        }
    }
    private Vector3[] _treadPositions = new Vector3[2];
    private GameObject[] _wheelsFront = new GameObject[2];
    private GameObject[] _wheelsBack = new GameObject[2];
    private bool _isSafetyBlockLeverEngage = false;
    [SerializeField] private float _speedUpDuration = 0.8f;

    private void RotateTread(Tread tread, Clockwise clockwise, float speed)
    {
        // Define the pivot point and rotation axis
        Vector3 pivotPoint = _treadPositions[((int)tread + 1) % 2];
        Vector3 rotationAxis = Vector3.up; // Rotate around the Y-axis

        // Calculate the rotation for this frame
        float angle = speed * Time.fixedDeltaTime * (int)clockwise; // Degrees to rotate this frame
        Quaternion rotation = Quaternion.AngleAxis(angle, rotationAxis); // Rotation quaternion

        // Calculate the object's new position around the pivot
        Vector3 directionToPivot = transform.position - pivotPoint; // Direction from the pivot to the object
        Vector3 newPosition = pivotPoint + rotation * directionToPivot;

        _rigidbody.MovePosition(newPosition);
        _rigidbody.MoveRotation(rotation * transform.rotation);
    }

    private void RotateBody()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            anim.SetFloat("RotateSpeed", 1f * _rotateBodyControlSpeed);
        }
        else if (!Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            anim.SetFloat("RotateSpeed", -1f * _rotateBodyControlSpeed);
        }
        else
        {
            anim.SetFloat("RotateSpeed", 0f);
        }
    }

    private void ControlShovel()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow) && anim.GetInteger("ShovelPosition") != 2)
        {
            anim.SetInteger("ShovelPosition", 1);
            anim.SetFloat("ShovelSpeed", 1f * _shovelControlSpeed);
        }
        else if (!Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow) && anim.GetInteger("ShovelPosition") != 0)
        {
            anim.SetInteger("ShovelPosition", 1);
            anim.SetFloat("ShovelSpeed", -1f * _shovelControlSpeed);
        }
        else
        {
            anim.SetFloat("ShovelSpeed", 0);
        }
    }

    private void MoveArm()
    {
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && anim.GetInteger("SmallArmPosition") != 2)
        {
            anim.SetInteger("SmallArmPosition", 1);
            anim.SetFloat("SmallArmSpeed", 1f * _smallArmControlSpeed);
        }
        else if (!Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S) && anim.GetInteger("SmallArmPosition") != 0)
        {
            anim.SetInteger("SmallArmPosition", 1);
            anim.SetFloat("SmallArmSpeed", -1f * _smallArmControlSpeed);
        }
        else
        {
            anim.SetFloat("SmallArmSpeed", 0);
        }
    }

    private void MoveBoom()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && anim.GetInteger("BigArmPosition") != 2)
        {
            anim.SetInteger("BigArmPosition", 1);
            anim.SetFloat("BigArmSpeed", 1f * _bigArmControlSpeed);
        }
        else if (!Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.DownArrow) && anim.GetInteger("BigArmPosition") != 0)
        {
            anim.SetInteger("BigArmPosition", 1);
            anim.SetFloat("BigArmSpeed", -1f * _bigArmControlSpeed);
        }
        else
        {
            anim.SetFloat("BigArmSpeed", 0);
        }
    }


}