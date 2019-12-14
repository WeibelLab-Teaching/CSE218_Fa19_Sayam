using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScript : MonoBehaviour
{

    public Transform pauseBtn;
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

            GameState.Instance.Playing = true;

            PlaySongNotes psn = (PlaySongNotes)keyboard.gameObject.GetComponent(typeof(PlaySongNotes));
            psn.playPauseSong();

            // hide the play button
            gameObject.SetActive(false);
            // show the pause button
            pauseBtn.gameObject.SetActive(true);
            
        }
    }
}
