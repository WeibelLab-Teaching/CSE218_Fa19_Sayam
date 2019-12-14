using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class SongItemHandler : MonoBehaviour
{
    public Transform songList;
    public Transform selectSongBtn;
    public Transform currentItem;
    public Transform keyboard;
    public Transform fullMenu;


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
        songList.gameObject.SetActive(false);
        GameState.Instance.currentSong = currentItem.Find("ButtonContent").Find("Label").GetComponent<TextMesh>().text;
        selectSongBtn.Find("ButtonContent").Find("Label").GetComponent<TextMesh>().text = GameState.Instance.truncate("Song: " + GameState.Instance.currentSong);
       
        //accessing the script on another object and calling a function
        PlaySongNotes play = (PlaySongNotes) keyboard.gameObject.GetComponent(typeof(PlaySongNotes));
        play.songSelected();
        

    }
}
