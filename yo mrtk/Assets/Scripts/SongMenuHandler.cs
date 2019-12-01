using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class SongMenuHandler : MonoBehaviour
{

    public Transform songList;
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
        songList.gameObject.SetActive(!songList.gameObject.activeSelf);
    }
}
