using UnityEngine;
using System.Collections;

public class FirstPersonControls : MonoBehaviour {

    //Variables
    public bool mouseVisible;
    public float movementSpeed = 5f;
    public float defaultMoveSpeed = 5f;
    public float sprintSpeed = 10.0f;
    public float mouseLookSPeed = 2.0f;
    public float mouseLockRange = 60.0f;
    public float jumpSpeed = 7.0f;
    public float jumpSpeedModifier = 2f;
    float lookUD = 0;
    float vertVelocity = 0;
    float forwardSpeed = 0f;
    float strafeSpeed = 0f;
    float jStrafeSpeed = 0f;
    public int grassDetectionTimer = 0;
    public static int chanceOfEncounter = 10;
    public Quaternion transRot = new Quaternion(0,0,0,0);
    SceneLoader sL = new SceneLoader();
    public Transform pokeScreen;
    jsonMod jm = new jsonMod();

    // Use this for initialization
    //Handling the Pokemon starter interface
    public void Button_ChoosePokemon_Clicked(int DexNo)
    {
        Intermediate.pokePlayer = jm.generateStarter(DexNo);
        pokeScreen.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    
    //Handling the load order
    void Start ()
    {
        jm.Start();
        if (Intermediate.returnFromSave == true)
        {
            transform.position = Intermediate.playerPos;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Intermediate.returnFromSave == false)
        {
            pokeScreen.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Cursor.visible == true)
        {
            mouseLookSPeed = 0f;
        }
        else
        {
            mouseLookSPeed = 2f;
        }

        //Debug.Log(Intermediate.playerPos);
        Intermediate.playerPos = transform.position;
        CharacterController cc = GetComponent<CharacterController>();

        //#################################Player Head Movement#################################

        float lookLR = Input.GetAxis("Mouse X") * mouseLookSPeed;
        transform.Rotate(0, lookLR, 0);
        lookUD -= Input.GetAxis("Mouse Y") * mouseLookSPeed;
        lookUD = Mathf.Clamp(lookUD, -mouseLockRange, mouseLockRange);
        Camera.main.transform.localRotation = Quaternion.Euler(lookUD, 0, 0);


        //#################################Character Directional Movement#################################

        //While Jumping
        if (cc.isGrounded == false)
        {
            vertVelocity += Physics.gravity.y * Time.deltaTime * 2;
            jStrafeSpeed = Input.GetAxis("Horizontal") / jumpSpeedModifier;
        }

        //While Grounded
        if (cc.isGrounded) {
            transRot = transform.rotation;
            forwardSpeed = Input.GetAxis("Vertical");
            strafeSpeed = Input.GetAxis("Horizontal");
            jStrafeSpeed = 0;
            if (Input.GetButtonDown("Jump"))
            {
                vertVelocity = jumpSpeed;
            }
        }
        //Sprinting
        if (Input.GetButton("Sprint"))
        {
            movementSpeed = sprintSpeed;
        }
        else
        {
            movementSpeed = defaultMoveSpeed;
        }

        //Crouching
        if (Input.GetButton("Crouch"))
        {
            Camera.main.transform.localPosition = new Vector3(0, 1.5f, 0);
            cc.transform.localScale = new Vector3(1, 0.75f, 1);
        }
        else
        {
            Camera.main.transform.localPosition = new Vector3(0, 2, 0);
            cc.transform.localScale = new Vector3(1, 1f, 1);
        };

        //Escape Menu

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            mouseVisible = !mouseVisible;
            if (mouseVisible) {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        //#################################Handling Output#################################
        Vector3 speed = new Vector3(jStrafeSpeed * movementSpeed + strafeSpeed * movementSpeed, vertVelocity, forwardSpeed * movementSpeed);
        speed = transRot * speed;
        cc.Move(speed * Time.deltaTime);

        //#################################Setting the chance of a pokemon encounter##############################################
        grassDetectionTimer = grassDetectionTimer + 1;
        if (grassDetectionTimer == 50)
        {
            //print("meme");
            grassDetectionTimer = 0;
            if (!(forwardSpeed == 0 & strafeSpeed == 0))
            {
                chanceOfEncounter = Random.Range(0, 3);
                print(chanceOfEncounter);
            }
        }
    }
}