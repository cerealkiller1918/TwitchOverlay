using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TestVideo : MonoBehaviour {

    private VideoPlayer videoPlayer;
    public bool play = true;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (videoPlayer.isPlaying && !play)
        {
            videoPlayer.Pause();
        }
        if(!videoPlayer.isPlaying && play)
        {
            videoPlayer.Play();
            play = false;
        }
    }

    public void PlayPause()
    {
        
    }
}
