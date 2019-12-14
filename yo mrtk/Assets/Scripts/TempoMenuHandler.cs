using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempoMenuHandler : MonoBehaviour
{

    public Transform tempoList;


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
        tempoList.gameObject.SetActive(!tempoList.gameObject.activeSelf);
        Debug.Log("tempo menu clicked!!!!!");
    }
}
