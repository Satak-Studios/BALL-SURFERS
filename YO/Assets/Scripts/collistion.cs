using UnityEngine;

public class collistion : MonoBehaviour  
{
    public Restart rs;
   
    
    public void OnCollisionEnter (Collision collisionInfo)

    

    {
        if (collisionInfo.collider.tag == "Obsticle")
        {
            FindObjectOfType<Restart>().EndGames();          
        }
    }
        
   
}
