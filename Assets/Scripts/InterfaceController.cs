using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController: MonoBehaviour
{
    public GameObject inventoryPanel;
    public Text itemText;
    bool invActive = false;
    public Image centroTela;

    void Start()
    {
        itemText.text = null;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            invActive =! invActive;
            inventoryPanel.SetActive(invActive);
        }
        if (invActive) 
        {
            centroTela.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            centroTela.gameObject.SetActive(true);
        }
    }
}
