using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardSong : MonoBehaviour
{
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
            Debug.Log("Song Forwarded");



            GameState.Instance.Forward = true;


        }
    }
}
