using UnityEngine;
using Photon.Pun;
using Satak.Utilities;

public class CompEndTrigger : MonoBehaviour
{
	public CompManager compManager;
	public int PlayerPosition = 0;
	
    void OnTriggerEnter ()
	{
		if (compManager.myPlayer.PV.IsMine)
		{
			compManager.CompleteCompAnim();
			SatakExtensions.AddPlayerPosition(compManager.myPlayer.PV.Owner, 1);
		}else
		{
			FindObjectOfType<Achiever>().Notify(compManager.myPlayer.PV.name, "Has Finnished the race");
		}
	}
}
