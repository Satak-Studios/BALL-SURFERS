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

    private void Start()
    {
        /*if (!isTestLevel)
        {
            forwardForce = PlayerPrefs.GetFloat("ff");
            sidewaysForce = PlayerPrefs.GetFloat("sf");
            Debug.Log("ff = " + forwardForce + " ," + "sf = " + sidewaysForce);
        }
        else
        {
            forwardForce = 300f;
            sidewaysForce = 35f;
        }*/

        if (PlayerPrefs.HasKey("ff") == true)
        {
            forwardForce = PlayerPrefs.GetFloat("ff");
            sidewaysForce = PlayerPrefs.GetFloat("sf");
            Debug.Log("ff = " + forwardForce + " ," + "sf = " + sidewaysForce);
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
            Debug.Log("ff = " + forwardForce + " ," + "sf = " + sidewaysForce);
        }
        else
        {
            float sf = 35;
            PlayerPrefs.SetFloat("sf", sf);
        }
    }

    // Update is called once per frame
    public void FixedUpdate()
     {
        if (gameStarted)
        {
            Movement();
        }
    }

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
            Debug.Log("Is Going Left");
            Left();
        }

        if (isgoingright)
        {
            Debug.Log("Is Going Right");
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

    public void SpawnOrg()
    {
        int randPlayer = Random.Range(0, playerPrefabs.Length);
        GameObject playertoSpawn = playerPrefabs[randPlayer];
        Instantiate(playertoSpawn , sp);
        Debug.Log("Spawned Player = " + playertoSpawn.name);
    }
}
