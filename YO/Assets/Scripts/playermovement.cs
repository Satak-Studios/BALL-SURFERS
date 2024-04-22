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
    private void Update()
    {
        Restart rs = FindObjectOfType<Restart>();
        if (rb.position.y < -1f)
        {
            if (!rs.isGodMode)
            {
                rs.EndGames(transform.position.z);
            }
            else
            {
                Transform pm = gameObject.transform;
                float x = 0f;
                float y = 1f;
                float z = pm.position.z;
                pm.position = new Vector3(x, y, z);
            }
        }
    }
    GameObject _obj;
    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obsticle")
        {
            FindObjectOfType<Restart>().EndGames(transform.position.z);
            _obj = collisionInfo.collider.gameObject;
            Invoke("Magic", 1f);
        }
    }

    void Magic()
    {
        Destroy(_obj);
    }
}
