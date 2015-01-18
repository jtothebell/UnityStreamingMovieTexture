using UnityEngine;
using System.Collections;
using Assets.Plugins;

public class movieGuiButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUILayout.Button("Play"))
        {
            gameObject.GetComponent<StreamingMovieTexture>().PlayRequested = true;
        }
    }
}
