using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController: MonoBehaviour
{
    // Invent�rio do player
    public Objects[] slotsInv;
    public Image[] slotImageInv;
    public Image[] quantidadeImageInv;
    public Image[] quantidadeFundoImageInv;
    public Text[] quantidadesTextInv;
    public int[] slotAmountInv;

    // Ba�
    int abriuBauCont = 0;
    bool abriuBau = false;
    public Sprite slotVazio;

    // Espa�o de um ba�
    public Objects[] slotsChest;
    public Image[] slotImageChest;
    public Image[] quantidadeImageChest;
    public Image[] quantidadeFundoImageChest;
    public Text[] quantidadesTextChest;
    public int[] slotAmountChest;

    // Invent�rio do player quando um ba� � aberto
    public Objects[] slotsChestInv;
    public Image[] slotImageChestInv;
    public Image[] quantidadeImageChestInv;
    public Image[] quantidadeFundoImageChestInv;
    public Text[] quantidadesTextChestInv;
    public int[] slotAmountChestInv;

    private float rangeRay = 5f;

    private InterfaceController iController;

    void Start()
    {
        iController = FindObjectOfType<InterfaceController>();
    }

    void Update()
    {
        // Fun��o que checa se um ba� foi aberto
        AbriuBau();

        // Raycast do aim do player
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        if (Physics.Raycast(ray, out hit, rangeRay)) 
        {
            if (hit.collider.tag == "Object")
            {
                iController.itemText.text = "Press (E) to collect the " + hit.transform.GetComponent<ObjectType>().objectype.name;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    for (int i = 0; i < slotsInv.Length; i++)
                    {

                        // Adiciona caso ja exista ou caso for o Item type igual adiciona mais um.
                        if (slotsInv[i] == null || slotsInv[i].name == hit.transform.GetComponent<ObjectType>().objectype.name)
                        {
                            // Adiciona ao slot o objeto em observa��o
                            slotsInv[i] = hit.transform.GetComponent<ObjectType>().objectype;
                            // Incrementa a quantidade do item caso ja tenha ou gera caso n tenha
                            slotAmountInv[i]++;
                            // Ativa a imagem da quantidade do item e seu fundo
                            quantidadeFundoImageInv[i].gameObject.SetActive(true);
                            quantidadeImageInv[i].gameObject.SetActive(true);
                            // Altera a quantidade descrita do item
                            quantidadesTextInv[i].GetComponent<Text>().text = slotAmountInv[i].ToString();
                            // Adiciona imagem a ele
                            slotImageInv[i].sprite = slotsInv[i].itemSprite;
                            Destroy(hit.transform.gameObject);
                            break;
                        }
                    }
                }
            } 
            else if(hit.collider.tag == "Chest")
            {
                if (abriuBauCont < 2)
                {
                    abriuBauCont++;
                }
                if (abriuBau == false && abriuBauCont == 2)
                {
                    iController.itemText.text = "Press (E) to open the Chest";

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        slotsChest = hit.transform.GetComponent<ChestItems>().itensChest;
                        slotAmountChest = hit.transform.GetComponent<ChestItems>().quantItensChest;
                        GameObject.FindGameObjectWithTag("Canvas").GetComponent<InterfaceController>().Chest(0);
                        abriuBau = true;
                    }
                }
            }
            else if(hit.collider.tag != "Object") 
            {
                iController.itemText.text = null;
            }
        }
        else
        {
            iController.itemText.text = null;
        }
    }

    void AbriuBau()
    {
        if(abriuBau == true)
        {/**
            for (int i = 0; i < slotsInv.Length; i++)
            {

            }**/

            if (abriuBauCont < 4)
            {
                SyncInventories(0);
                abriuBauCont++;
            }
            // Desativar o menu de ba�
            if(Input.GetKeyDown(KeyCode.E) && abriuBauCont == 4)
            {
                abriuBauCont = 0;
                GameObject.FindGameObjectWithTag("Canvas").GetComponent<InterfaceController>().Chest(1);
                SyncInventories(1);
                abriuBau = false;
            }
        }
    }


    // Fun��o que vai atualizar o invent�rio que aparece ao abrir ba�s com o invent�rio normal. Ao receber os seguintes n�meros:
    // 0: Atualiza o invent�rio para b�u
    // 1: Atualiza o invent�rio normal
    void SyncInventories(int invParaAtualizar)
    {
        if(invParaAtualizar == 0)
        {
            for (int i = 0; i < slotsInv.Length; i++)
            {
                if (slotsInv[i] != null)
                {
                    slotsChestInv[i] = slotsInv[i];
                    slotAmountChestInv[i] = slotAmountInv[i];
                    quantidadeFundoImageChestInv[i].gameObject.SetActive(true);
                    quantidadeImageChestInv[i].gameObject.SetActive(true);
                    quantidadesTextChestInv[i].GetComponent<Text>().text = slotAmountChestInv[i].ToString();
                    slotImageChestInv[i].sprite = slotsInv[i].itemSprite;
                }
            }

            for (int i = 0; i < slotsChest.Length; i++)
            {
                if (slotsChest[i] != null && slotAmountChest[i] != 0)
                {
                    quantidadeFundoImageChest[i].gameObject.SetActive(true);
                    quantidadeImageChest[i].gameObject.SetActive(true);
                    quantidadesTextChest[i].GetComponent<Text>().text = slotAmountChest[i].ToString();
                    slotImageChest[i].sprite = slotsChest[i].itemSprite;
                }
                else
                {
                    quantidadeFundoImageChest[i].gameObject.SetActive(false);
                    quantidadeImageChest[i].gameObject.SetActive(false);
                    slotImageChest[i].sprite = slotVazio;
                }
            }

        }
        else
        {
            for (int i = 0; i < slotsChestInv.Length; i++)
            {
                if (slotsChestInv[i] != null)
                {
                    slotsInv[i] = slotsChestInv[i];
                    slotAmountInv[i] = slotAmountChestInv[i];
                    quantidadeFundoImageInv[i].gameObject.SetActive(true);
                    quantidadeImageInv[i].gameObject.SetActive(true);
                    quantidadesTextInv[i].GetComponent<Text>().text = slotAmountInv[i].ToString();
                    slotImageInv[i].sprite = slotsChestInv[i].itemSprite;
                }
                else
                {
                    quantidadeFundoImageInv[i].gameObject.SetActive(false);
                    quantidadeImageInv[i].gameObject.SetActive(false);
                    slotImageInv[i].sprite = slotVazio;
                }
            }
        }
    }

    // Fun��o que ao ser chamada permite a transfer�ncia de itens.
    // Caso lado seja 0, o slot est� do aldo do invent�rio do ba�
    // Caso seja 1, est� no pr�prio ba�
    public void TransferirItem(int slotNum, int lado)
    {
        if(lado == 0)
        {
            // A cada scroll para cima
            if(Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (slotAmountChestInv[slotNum - 1] != 0)
                {
                    int slotChestNum = 9;
                    slotAmountChestInv[slotNum - 1] = slotAmountChestInv[slotNum - 1] - 1;
                    quantidadesTextChestInv[slotNum - 1].GetComponent<Text>().text = slotAmountChestInv[slotNum - 1].ToString();
                    if(slotAmountChestInv[slotNum - 1] == 0)
                    {
                        quantidadeFundoImageChestInv[slotNum - 1].gameObject.SetActive(false);
                        quantidadeImageChestInv[slotNum - 1].gameObject.SetActive(false);
                        slotImageChestInv[slotNum - 1].sprite = slotVazio;
                    }
                    for (int i = 0; i < slotsChest.Length; i++)
                    {
                        // Se o objeto j� existir dentro do ba�
                        if(slotsChest[i] == slotsChestInv[slotNum - 1])
                        {
                            slotChestNum = i;
                            i = slotsChest.Length;
                        }
                        // Se o objeto n�o existir, pega o primeiro slot nulo
                        else if (slotsChest[i] == null)
                        {
                            slotChestNum = i;
                            i = slotsChest.Length;
                        }
                    }
                    if(slotChestNum != 9)
                    {
                        slotAmountChest[slotChestNum] += 1;
                        quantidadesTextChest[slotChestNum].GetComponent<Text>().text = slotAmountChest[slotChestNum].ToString();
                    }

                        /* Invent�rio do player quando um ba� � aberto
                        public Objects[] slotsChestInv;
                        public Image[] slotImageChestInv;
                        public Image[] quantidadeImageChestInv;
                        public Image[] quantidadeFundoImageChestInv;
                        public Text[] quantidadesTextChestInv;
                        public int[] slotAmountChestInv;*/
                    }
               
            }
        }
        else
        {
        }
    }

}
