using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartMenu : MonoBehaviour
{
    playermovement playerMovement;
    public GameObject Continue_btn;
    int katam;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindObjectOfType<playermovement>();
    }

    private void Update()
    {
        katam = PlayerPrefs.GetInt("band");
        if (!playerMovement.isIntro)
        {
            if (katam >= 3 && SceneManager.GetActiveScene().buildIndex <= 2)
            {
                Continue_btn.SetActive(true);
            }
            else
            {
                Continue_btn.SetActive(false);
            }
        }
        else
        {
            Continue_btn.SetActive(true);
        }
    }

    public void Continue()
    {
        playerMovement.LoadPos();
        FindObjectOfType<playermovement>().enabled = true;
        FindObjectOfType<playermovement>().rb.velocity = new Vector3(0,0,0);
        Restart rs = FindObjectOfType<Restart>();
        rs.Restartmenu.SetActive(false);
        rs.gameHasEnded = false;
        Time.timeScale = 1;
        rs.isInv = true;
        Invoke("NoInv", 3f);
    }

    public void NoInv()
    {
        FindObjectOfType<Restart>().isInv = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("sdrn", 0);
    }

    public void LoadingMenu()
    {
        SceneManager.LoadScene("Menu 1");
        PlayerPrefs.SetInt("sdrn", 0);
    }
}
