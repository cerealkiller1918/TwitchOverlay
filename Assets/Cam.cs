using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cam : MonoBehaviour {

    private bool camAvailable;
    private WebCamTexture backCam;
    private Texture defaultBackground;
    public RawImage backGround;
    public AspectRatioFitter fit;

	// Use this for initialization
	void Start () {
        defaultBackground = backGround.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            Debug.Log("No camera detected");
            camAvailable = false;
            return;
        }



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
