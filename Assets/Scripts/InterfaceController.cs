using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController: MonoBehaviour
{
    // Para o inventário do player
    public GameObject inventoryPanel;
    bool invActive = false;

    // Para o inventário do player e espaço do chest ao abrir um
    public GameObject chestPanel;
    public GameObject chestInventoryPanel;
    public GameObject HUDPanel;
    public Image centroTela;
    public Text itemText;

    void Start()
    {
        itemText.text = null;
    }
    void Update()
    {
        abrirInventario();
    }

    void abrirInventario()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            invActive = !invActive;
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

    public void Chest(int ligarDes)
    {   
        if (ligarDes == 0)
        {
            HUDPanel.SetActive(false);
            chestPanel.SetActive(true);
            chestInventoryPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IfCanMove(1);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonCam>().IfCanMove(1);
        }
        else
        {
            chestPanel.SetActive(false);
            chestInventoryPanel.SetActive(false);
            HUDPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            centroTela.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().IfCanMove(0);
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FirstPersonCam>().IfCanMove(0);
        }
    }
}
