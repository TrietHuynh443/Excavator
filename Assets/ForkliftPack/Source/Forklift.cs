using UnityEngine;

public class Forklift : MonoBehaviour {

    //INFO@GABROMEDIA.COM

    Transform fork;
    Transform chainRollers;
    Transform forkMechanism;

    Material chainMat;

    const float forkMaxUp = 2f;
    float forkMaxDown;

    public KeyCode raiseForkKeyCode;
    public KeyCode lowerForkKeyCode;
    public KeyCode bendMechanismIn;
    public KeyCode bendMechanismOut;

    //Animator

    float rotSpeed = 10f;
    public float driveSpeed = 3f;
    public float scrollSpeed = 0.5f;


    float Speed = 0;//Don't touch this
    float MaxSpeed = 110;//This is the maximum speed that the object will achieve
    float Acceleration = 13;//How fast will object reach a maximum speed
    float Deceleration = 13;//How fast will object reach a speed of 0

    float direction = 0;


    //Big Wheels
    public GameObject WheelFrontLeft;
    public GameObject WheelFrontRight;
    public GameObject WheelBackLeft;
    public GameObject WheelBackRight;



    void Start() {


        //Search children based on MeshFilter components (they all have it)
        foreach (var mf in GetComponentsInChildren<MeshFilter>()) {
            //Find fork
            if (mf.name.Equals("Fork")) {
                fork = mf.transform;
                forkMaxDown = fork.transform.localPosition.z;
            }
            //Find fork mechanism, when found, store Chain material
            if (mf.name.Equals("Fork_Mechanism")) {
                forkMechanism = mf.transform;
                Renderer r = mf.GetComponent<Renderer>();
                foreach (var m in r.materials) {
                    if (m.name.Contains("Chain")) {
                        chainMat = m;
                    }
                }
            }
            //Rollers
            if (mf.name.Equals("Chain_Rollers")) {
                chainRollers = mf.transform;
            }
        }

    }

    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow) && (Speed < MaxSpeed))
        {
            Speed = Speed + Acceleration * Time.deltaTime;
            direction = 1;
        }

        else if (Input.GetKey(KeyCode.DownArrow) && (Speed < MaxSpeed))
        {
            Speed = Speed + Acceleration * Time.deltaTime;
            direction = 2;
        }

        else {

            if (Speed > Deceleration * Time.deltaTime) Speed = Speed - Deceleration * Time.deltaTime;
            else if (Speed < -Deceleration * Time.deltaTime) Speed = Speed + Deceleration * Time.deltaTime;
            else
                Speed = 0;

        }

        if (Speed == 0) direction = 0;
        if (Input.GetKey(KeyCode.Space)) Speed = 0;

        print(Speed);

                
    }




    void FixedUpdate()
    {
        //print(GetComponent<Rigidbody>().velocity.magnitude);

        //Move fork up and down on local axis, set offset for chain material, rotate the rollers
        if (Input.GetKey(raiseForkKeyCode))
        {
            if (fork.transform.localPosition.z <= forkMaxUp)
            {
                fork.transform.localPosition += Vector3.forward * Time.deltaTime;
                chainMat.mainTextureOffset = new Vector2(chainMat.mainTextureOffset.x - Time.deltaTime, chainMat.mainTextureOffset.y);
                chainRollers.Rotate(Vector3.right * 6);
            }
        }
        if (Input.GetKey(lowerForkKeyCode))
        {
            if (fork.transform.localPosition.z >= forkMaxDown)
            {
                fork.transform.localPosition -= Vector3.forward * Time.deltaTime;
                chainMat.mainTextureOffset = new Vector2(chainMat.mainTextureOffset.x + Time.deltaTime, chainMat.mainTextureOffset.y);
                chainRollers.Rotate(-Vector3.right * 6);
            }
        }

        //Tilt the mechanism
        if (Input.GetKey(bendMechanismIn))
        {
            if (forkMechanism.localEulerAngles.x < 275f)
            {
                forkMechanism.Rotate(Vector3.right * Time.deltaTime * 5);
            }

        }
        if (Input.GetKey(bendMechanismOut))
        {
            if (forkMechanism.localEulerAngles.x > 270f)
            {
                forkMechanism.Rotate(-Vector3.right * Time.deltaTime * 5);
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            
            transform.RotateAround(WheelFrontRight.transform.position, Vector3.up, Speed * Time.deltaTime);
            transform.RotateAround(WheelFrontLeft.transform.position, -Vector3.up, Speed * Time.deltaTime);
            
        }


        if (direction == 1)
        {

            transform.RotateAround(WheelFrontRight.transform.position, Vector3.up, Speed * Time.deltaTime);
            transform.RotateAround(WheelFrontLeft.transform.position, -Vector3.up, Speed * Time.deltaTime);

        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.RotateAround(WheelFrontRight.transform.position, -Vector3.up, Speed * Time.deltaTime);
            transform.RotateAround(WheelFrontLeft.transform.position, Vector3.up, Speed * Time.deltaTime);
        }


        if (direction == 2)
        {
            transform.RotateAround(WheelFrontRight.transform.position, -Vector3.up, Speed * Time.deltaTime);
            transform.RotateAround(WheelFrontLeft.transform.position, Vector3.up, Speed * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.RotateAround(WheelFrontRight.transform.position, -Vector3.up, Time.deltaTime * rotSpeed * 2);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.RotateAround(WheelFrontLeft.transform.position, Vector3.up, Time.deltaTime * rotSpeed * 2);
        }

        

    }
}





