using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator animator;
    int levelHash;
 
    

    public  void play()
    {
        animator = GetComponent<Animator>();
    }
}

