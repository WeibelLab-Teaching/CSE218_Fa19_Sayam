using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class TempoListBootstrap : MonoBehaviour
{
    public Transform temposList;
    public Transform tempSelectBtn;
    public Transform tempoItemPrefab;
    public Transform keyboard;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Inside TempoListBootstrap Start");
        for (int i = 0; i < GameState.Instance.tempos.Length; i++)
        {
            Transform item = Object.Instantiate(tempoItemPrefab, temposList);

            item.Find("ButtonContent").Find("Label").GetComponent<TextMesh>().text = GameState.Instance.tempos[i];
            item.position = new Vector3(temposList.position.x, temposList.position.y - 0.1f - i * 0.1f, temposList.position.z);

            item.GetComponent<TempoItemHandler>().tempoList = temposList;
            item.GetComponent<TempoItemHandler>().currentItem = item;
            item.GetComponent<TempoItemHandler>().tempSelectBtn = tempSelectBtn;

            item.GetComponent<Interactable>().OnClick.AddListener(item.GetComponent<TempoItemHandler>().OnAirTap);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
