using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class SongsListBootstrap : MonoBehaviour
{
    public Transform songsList;
    public Transform songItemPrefab;
    public Transform keyboard;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i < GameState.Instance.songs.Length; i++)
        {
            Transform item = Object.Instantiate(songItemPrefab);
            Debug.Log(item);
            Debug.Log(item.Find("ButtonContent"));
            Debug.Log(item.Find("ButtonContent").Find("Label"));

            item.Find("ButtonContent").Find("Label").GetComponent<TextMesh>().text = GameState.Instance.songs[i];
            item.parent = songsList;
            item.position = new Vector3(songsList.position.x, songsList.position.y - 0.1f - i * 0.1f, songsList.position.z);

            item.GetComponent<SongItemHandler>().songList = songsList;
            item.GetComponent<SongItemHandler>().keyboard = keyboard;
            item.GetComponent<SongItemHandler>().currentItem = item;

            item.GetComponent<Interactable>().OnClick.AddListener(item.GetComponent<SongItemHandler>().OnAirTap);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
