using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform po;
    public Transform player = null;
    public bool isOnline = false;
    public Vector3 offset;
    public bool Comp = false;
    
    // Update is called once per frame
    void Update()
    {
        if (isOnline == true)
        {
            po = FindObjectOfType<PlayerOnline>().transform;
            transform.position = po.position + offset;
        }
        else
        {
            if (player == null)
            {
                player.position = FindObjectOfType<playermovement>().transform.position;
            }
            transform.position = player.position + offset;
        }        
    }

    public void CompleteCompReal()
    {
        if (Comp)
        {
            FindObjectOfType<CompManager>().CompleteComp();
        }
    }
}
