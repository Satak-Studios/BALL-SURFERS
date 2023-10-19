﻿using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform po;
    public Transform player;
    public bool isOnline = false;
    public Vector3 offset;
    public bool Comp = false;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
        if (isOnline == true)
        {
            po = FindObjectOfType<PlayerOnline>().transform;
            transform.position = po.position + offset;
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
