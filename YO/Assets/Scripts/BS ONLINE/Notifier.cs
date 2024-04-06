using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Notifier : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject notificationPanel;
    [SerializeField] private Text notificationText;
    [SerializeField] private Animator _animator;

    private bool isNotificationShowing = false;

    private const string notifyParam = "notify";

    void Start()
    {
        if (notificationPanel != null)
            notificationPanel.SetActive(false);
    }

    [PunRPC]
    private void ShowNotification(string senderName, string message)
    {
        if (!isNotificationShowing)
        {
            isNotificationShowing = true;
            notificationText.text = "<b>" + senderName + "</b>" + message;
            notificationPanel.SetActive(true);
            _animator.SetBool(notifyParam, true);
            Debug.Log("Received notification from " + senderName + ": " + message);
            Invoke(nameof(ResetNotification), 6f);
        }
    }

    public void SendInfo(string message)
    {
        string senderName = PhotonNetwork.LocalPlayer.NickName;
        photonView.RPC("ShowNotification", RpcTarget.Others, senderName, message);
    }

    public void ResetNotification()
    {
        isNotificationShowing = false;
        _animator.SetBool(notifyParam, false);
        notificationPanel.SetActive(false);
    }
}