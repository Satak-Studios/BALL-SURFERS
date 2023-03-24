using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointTrigger : MonoBehaviour
{
    public player scoreTimer;
    public GameObject cPlayerPos;
    public float pPos_X; 
    public float pPos_Y; 
    public float pPos_Z;    
    public Vector3 pos_;
    public float currentScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreTimer = FindObjectOfType<player>();
        cPlayerPos = FindObjectOfType<PlayerOnline>().gameObject;
        if (cPlayerPos == null)
        {
            return;
        }
        pos_.x = PlayerPrefs.GetFloat("xPos");
        pos_.y = PlayerPrefs.GetFloat("yPos");
        pos_.z = PlayerPrefs.GetFloat("zPos");
        currentScore = PlayerPrefs.GetFloat("scoreo");
    }

    public void OnTriggerEnter()
    {
       //pPos[1] = cPlayerPos.transform.position.x;
       //pPos[2] = cPlayerPos.transform.position.y;
       //pPos[3] = cPlayerPos.transform.position.z;

       pPos_X = cPlayerPos.transform.position.x;
       pPos_Y = cPlayerPos.transform.position.y;
       pPos_Z = cPlayerPos.transform.position.z;
       //Curren Timer(Score);
       currentScore = scoreTimer.currentTime;

       pos_.x = pPos_X;
       pos_.y = pPos_Y;
       pos_.z = pPos_Z;
       PlayerPrefs.SetFloat("xPos", pPos_X);
       PlayerPrefs.SetFloat("yPos", pPos_Y);
       PlayerPrefs.SetFloat("zPos", pPos_Z);

       PlayerPrefs.SetFloat("scoreo", currentScore);
       Debug.Log("checkPoint Saved and they are: " + "x = " + pPos_X + ", y = " + pPos_Y + ", z = " + pPos_Z);
    }

    public void LoadFromPreviousCheckpoint(){
       //pos_.x = pPos[1];
       //pos_.y = pPos[2];
       //pos_.z = pPos[3];
       cPlayerPos.transform.position = pos_;

       //cPlayerPos.position.x = pPos[1];
       //cPlayerPos.position.y = pPos[2];
       //cPlayerPos.position.z = pPos[3];
}
}
