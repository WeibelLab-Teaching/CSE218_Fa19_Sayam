using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongItemHandler : MonoBehaviour
{
    public Transform songList;
    public Transform currentItem;
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
        songList.gameObject.SetActive(false);
        GameState.Instance.currentSong = currentItem.Find("ButtonContent").Find("Label").GetComponent<TextMesh>().text;
        if (!keyboard.gameObject.activeSelf)
        {
            keyboard.gameObject.SetActive(true);
        }

    }
}
