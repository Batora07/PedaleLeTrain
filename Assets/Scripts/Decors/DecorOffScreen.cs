using UnityEngine;
using System.Collections;

public class DecorOffScreen : MonoBehaviour {
    public PoleSpawner ps;
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Pole")
        {
            ps.PoleDisappeared(other.gameObject);
        }
    }
}
