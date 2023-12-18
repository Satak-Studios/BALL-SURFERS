using UnityEngine;

public class CompEndTrigger : MonoBehaviour
{
	public CompManager compManager;
	public int PlayerPosition = 0;
	public bool isComp = false;
	
    void OnTriggerEnter ()
	{
		if (compManager.myPlayer.PV.IsMine && isComp)
		{
			compManager.CompleteCompAnim();
			FindObjectOfType<Achiever>().Notify(compManager.myPlayer.PV.name, "Has Finished the race");
			Debug.Log(compManager.myPlayer.PV.name + "Has Finished the race");
		}
	}
}
