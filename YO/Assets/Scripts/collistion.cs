using UnityEngine;

public class collistion : MonoBehaviour  
{
    public Restart rs;
    GameObject _obj;

    public void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obsticle")
        {
            FindObjectOfType<Restart>().EndGames();
            _obj = collisionInfo.collider.gameObject;
            Invoke("Magic", 1f);
        }
    }   

    void Magic()
    {
        Destroy(_obj);
    }
}
