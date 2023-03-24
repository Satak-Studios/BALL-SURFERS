using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class cam : MonoBehaviour
{
    public GameObject Player_Name_Obj = null;
    public GameObject Player_Cam = null;
    public Text Player_name = null;
    public PhotonView PV;

    void Update()
    {
        if (!PV.IsMine)
        {
            Destroy(Player_Cam);
            Player_Name_Obj.SetActive(true);
        }
        else
        {
            //Destroy(Player_Name_Obj);
            Player_Name_Obj.SetActive(false);
        }
        Player_name.text = PV.Owner.NickName;
    }
}