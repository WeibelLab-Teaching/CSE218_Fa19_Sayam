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
        if (!songList.gameObject.activeSelf)
        {
            songList.transform.position = this.transform.position;
            songList.transform.rotation = this.transform.rotation;
            songList.gameObject.SetActive(true);

            // disable billboarding and radial view
            fullMenu.gameObject.GetComponent<Billboard>().enabled = false;
            fullMenu.gameObject.GetComponent<RadialView>().enabled = false;

        } else
        {
            songList.gameObject.SetActive(false);
        }

        Debug.Log("Song menu clicked!!!!!");
    }
}
