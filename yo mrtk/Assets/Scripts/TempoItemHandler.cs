using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoItemHandler : MonoBehaviour
{
    public Transform tempoList;
    public Transform tempSelectBtn;
    public Transform currentItem;

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
        tempoList.gameObject.SetActive(false);
        GameState.Instance.currentTempo = currentItem.Find("ButtonContent").Find("Label").GetComponent<TextMesh>().text;
        tempSelectBtn.Find("ButtonContent").Find("Label").GetComponent<TextMesh>().text = GameState.Instance.truncate("Tempo: " + GameState.Instance.currentTempo);
    }
}

