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
    bool chestActive = false;

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

    public void Chest()
    {
        chestActive = !chestActive;
        chestPanel.SetActive(chestActive);
        chestInventoryPanel.SetActive(chestActive);

        if (chestActive)
        {
            centroTela.gameObject.SetActive(false);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1;
            centroTela.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
