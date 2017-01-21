using UnityEngine;
using System.Collections;

// class used to rescale the background for multiple resolutions
public class BGScaler : MonoBehaviour {

	void Start () {
        // calculate the height
        var height = Camera.main.orthographicSize * 2f;
        var width = height * Screen.width / Screen.height;

        if(gameObject.name == "Background")
        {
            transform.localScale = new Vector3(width, height, 0); 
        }
        else
        {
            // Second parameter is for the fixed bottom ground / train
            transform.localScale = new Vector3(width + 3f, 5, 0);
        }
    }
	
}
