using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    public Transform playBtn;
    public Transform keyboard;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnAirTap()
    {

        {

            GameState.Instance.Playing = false;
            // hide the pause button
            gameObject.SetActive(false);
            // show the play button
            playBtn.gameObject.SetActive(true);


            PlaySongNotes psn = (PlaySongNotes)keyboard.gameObject.GetComponent(typeof(PlaySongNotes));
            psn.playPauseSong();
        }
    }
}
