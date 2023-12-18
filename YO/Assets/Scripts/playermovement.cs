using UnityEngine;

public class playermovement : MonoBehaviour
{
    public bool isTestLevel;

    public Rigidbody rb;

    public bool isgoingleft;
    public bool isgoingright;

    public LevelGenerator lg;

    //Player Speed
    public float forwardForce;
    public float sidewaysForce;

    public GameObject[] playerPrefabs;
    public Transform sp;

    public bool gameStarted = true;

    public Restart rs;

    void Start()
    {
        if (PlayerPrefs.HasKey("ff") == true)
        {
            forwardForce = PlayerPrefs.GetFloat("ff");
            sidewaysForce = PlayerPrefs.GetFloat("sf");
        }
        else
        {
            float ff = 300;
            PlayerPrefs.SetFloat("ff", ff);
        }

        if (PlayerPrefs.HasKey("sf") == true)
        {
            forwardForce = PlayerPrefs.GetFloat("ff");
            sidewaysForce = PlayerPrefs.GetFloat("sf");
        }
        else
        {
            float sf = 35;
            PlayerPrefs.SetFloat("sf", sf);
        }
        rs = FindObjectOfType<Restart>();
    }

    // Update is called once per frame
    public void FixedUpdate()
     {
        if (gameStarted)
        {
            Movement();
        }
    }

    #region Movement
    public void Movement()
    {
        // Add a forward force
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        //Controls for A & D
        if (Input.GetKey("d"))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        //Controls for Arrow Keys
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }


        if (isgoingleft)
        {
            Left();
        }

        if (isgoingright)
        {
            Right();
        }
    }

    public void Left()
    {
        rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }
    public void Right()
    {
        rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
    }

    public void pointerDownleft()
    {
        isgoingleft = true;
    }
    public void pointerUpleft()
    {
        isgoingleft = false;
    }
    public void pointerDownright()
    {
        isgoingright = true;
    }
    public void pointerUpright()
    {
        isgoingright = false;
    }
#endregion

    public void LoadPos()
    {
        transform.position = rs.katamPoint;
    }
}
