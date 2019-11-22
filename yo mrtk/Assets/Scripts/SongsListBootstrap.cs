using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class SongsListBootstrap : MonoBehaviour
{
    public Transform songsList;
    public Transform selectSongBtn;
    public Transform songItemPrefab;
    public Transform keyboard;
    public Transform fullMenu;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Inside SongsListBootstrap Start");
        for (int i=0; i < GameState.Instance.songs.Length; i++)
        {
            Transform item = Object.Instantiate(songItemPrefab, songsList);
            Debug.Log(item);
            Debug.Log(item.Find("ButtonContent"));
            Debug.Log(item.Find("ButtonContent").Find("Label"));

            item.Find("ButtonContent").Find("Label").GetComponent<TextMesh>().text = GameState.Instance.songs[i];
            item.position = new Vector3(songsList.position.x, songsList.position.y - 0.1f - i * 0.1f, songsList.position.z);

            item.GetComponent<SongItemHandler>().songList = songsList;
            item.GetComponent<SongItemHandler>().keyboard = keyboard;
            item.GetComponent<SongItemHandler>().currentItem = item;
            item.GetComponent<SongItemHandler>().selectSongBtn = selectSongBtn;
            item.GetComponent<SongItemHandler>().fullMenu = fullMenu;

            //item.gameObject.AddComponent<RadialView>();
            //item.GetComponent<RadialView>().MaxViewDegrees = 5;
            //item.gameObject.AddComponent<Billboard>();
            //item.GetComponent<Billboard>().PivotAxis = Microsoft.MixedReality.Toolkit.Utilities.PivotAxis.Free;

            item.GetComponent<Interactable>().OnClick.AddListener(item.GetComponent<SongItemHandler>().OnAirTap);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
