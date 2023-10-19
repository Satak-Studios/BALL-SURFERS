using UnityEngine;
using Photon.Pun;
using Satak.Utilities;

public class CompEndTrigger : MonoBehaviour
{
	public CompManager compManager;
	public int PlayerPosition = 0;
	
    void OnTriggerEnter ()
	{
		PhotonView PV = compManager.myPlayer.PV;
		if (PV.IsMine)
		{
			compManager.CompleteCompAnim();
			SatakExtensions.AddPlayerPosition(compManager.myPlayer.PV.Owner, 1);
			Debug.LogError("compu!!!");
		}
	}
}
