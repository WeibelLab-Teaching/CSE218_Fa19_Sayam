using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPause : MonoBehaviour
{
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
            Debug.Log("Playing changed");



            GameState.Instance.Playing = !GameState.Instance.Playing;


            PlaySongNotes psn = (PlaySongNotes)keyboard.gameObject.GetComponent(typeof(PlaySongNotes));
            psn.playPauseSong();
        }
    }
}
