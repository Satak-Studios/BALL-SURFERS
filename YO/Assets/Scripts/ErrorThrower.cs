using UnityEngine;
using UnityEngine.UI;

public class ErrorThrower : MonoBehaviour
{
    public GameObject errorObj;
    public Text errorCode;
    public Text errorText;
    public Text errorTitle;

    public bool networkAvailable = false;

    void Start()
    {
        errorObj.SetActive(false);
        CheckInternet();
    }

    public void ThrowError(string returnCode, string message, string title)
    {
        errorObj.SetActive(true);
        errorCode.text = returnCode.ToString();
        errorText.text = message;
        errorTitle.text = title;
    }

    public void CheckInternet()
    {
        NetworkReachability reachability = Application.internetReachability;

        switch (reachability)
        {
            case NetworkReachability.NotReachable:
                ThrowError("404_:(", "Make sure you are connected to the internet.", "Oops!");
                networkAvailable = false;
            break;

            case NetworkReachability.ReachableViaCarrierDataNetwork:
                ThrowError("🤨DataNetwork?🤨", "Are you sure to play BALL SURFERS using your Mobile Data?", "Sure?");
            networkAvailable = true;
            break;

            case NetworkReachability.ReachableViaLocalAreaNetwork:
                networkAvailable = true;
            break;
        }
    }
}
