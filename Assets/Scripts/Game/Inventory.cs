using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.U2D.Common;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public bool inventoryEnabled;
    GameObject backGroundPanel;

    GameObject invt, invtAmulets, map;

    int allSlots;
    GameObject[] slots, keys, amulets;
    GameObject slotsInventory, slotsKeys, slotsAmulets, slotsAmlsEq;

    Text titleInv, title, description, dirLeft, dirRight;

    GameController gameCont;
    GameObject moneyMarker, deco_3, deco_4, pickaxeInv, selector;

    GameObject slotPrefab, groovePrefab;

    int levelPickaxe;

    void Awake()
    {
        Debug.Log("Awake Inventory");

        gameCont = GameController.Instance;

        slotPrefab = Resources.Load<GameObject>("Prefabs/slot");
        groovePrefab = Resources.Load<GameObject>("Prefabs/groove");

        backGroundPanel = transform.GetChild(0).gameObject;
        invt = backGroundPanel.transform.Find("inventory").gameObject;
        invtAmulets = backGroundPanel.transform.Find("invtAmulets").gameObject;
        map = backGroundPanel.transform.Find("map").gameObject;
        deco_3 = backGroundPanel.transform.Find("deco_3").gameObject;
        deco_4 = backGroundPanel.transform.Find("deco_4").gameObject;
        selector = backGroundPanel.transform.Find("selector").gameObject;

        titleInv = backGroundPanel.transform.Find("titleInvt").gameObject.GetComponent<Text>();
        titleInv.text = "Inventario";

        dirLeft = backGroundPanel.transform.Find("dirLeft").gameObject.GetComponent<Text>();
        dirLeft.text = "Mapa";

        dirRight = backGroundPanel.transform.Find("dirRight").gameObject.GetComponent<Text>();
        dirRight.text = "Amuletos";

        title = backGroundPanel.transform.Find("title").gameObject.GetComponent<Text>();
        description = backGroundPanel.transform.Find("description").gameObject.GetComponent<Text>();
        
        slotsInventory = invt.transform.Find("slotsInventory").gameObject;
        slotsKeys = invt.transform.Find("slotsKeys").gameObject;
        slotsAmulets = invtAmulets.transform.Find("slotsAmulets").gameObject;
        slotsAmlsEq = invtAmulets.transform.Find("amuletsEquip").gameObject;

        moneyMarker = invt.transform.Find("moneyMarker").gameObject;


        inventoryEnabled = false;
        backGroundPanel.SetActive(false);
        invtAmulets.SetActive(false);
        map.SetActive(false);
        selector.SetActive(false);


        // Resto de objetos
        allSlots = 6;
        slots = new GameObject[allSlots];

        slots[0] = Instantiate(slotPrefab, new Vector3(-225, 90, 0), Quaternion.identity);
        slots[1] = Instantiate(slotPrefab, new Vector3(-280, -60, 0), Quaternion.identity);
        slots[2] = Instantiate(slotPrefab, new Vector3(-190, -60, 0), Quaternion.identity);
        slots[3] = Instantiate(slotPrefab, new Vector3(-60, 90, 0), Quaternion.identity);
        slots[4] = Instantiate(slotPrefab, new Vector3(60, 90, 0), Quaternion.identity);
        slots[5] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        for (int i = 0; i < allSlots; i++)
        {
            slots[i].transform.SetParent(slotsInventory.transform, false);
        }

        // Llaves
        allSlots = 4;
        keys = new GameObject[allSlots];

        keys[0] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        keys[1] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        keys[2] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        keys[3] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        for (int i = 0; i < allSlots; i++)
        {
            keys[i].transform.SetParent(slotsKeys.transform, false);
        }

        // Amuletos
        allSlots = 9;
        amulets = new GameObject[allSlots];

        amulets[0] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        amulets[1] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        amulets[2] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        amulets[3] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        amulets[4] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        amulets[5] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        amulets[6] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        amulets[7] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        amulets[8] = Instantiate(slotPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        for (int i = 0; i < allSlots; i++)
        {
            amulets[i].transform.SetParent(slotsAmulets.transform, false);
        }


        pickaxeInv = Instantiate(slotPrefab, new Vector3(-230, 30, 0), Quaternion.identity);
        pickaxeInv.transform.SetParent(invtAmulets.transform, false);


        // Objetos
        levelPickaxe = -1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;

            if (inventoryEnabled)
            {
                UpdateTitDescp("", "");

                // Activamos el panel del inventario y obtenemos el dinero a mostrar
                backGroundPanel.SetActive(true);
                moneyMarker.transform.GetChild(1).GetComponent<Text>().text =
                    gameCont.GetMoney().ToString();
            }
            else
            {   // Desactivamos el panel del inventario
                backGroundPanel.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdatePickaxe();
        }
    }

    

    public void AddObjectInvt(int pos, string ntit, string descpt)
    {
        if (slots[pos].GetComponent<Slot>().empty)
        {
            slots[pos].GetComponent<Slot>().UpdateSlotInvt(pos, ntit, descpt);
        }
    }

    public void AddKeyInvt(int pos, string ntit, string descpt)
    {
        if (keys[pos].GetComponent<Slot>().empty)
        {
            keys[pos].GetComponent<Slot>().UpdateSlotKeys(pos, ntit, descpt);
        }
    }

    public void ActiveSlotAmulet(int pos, string ntit, string descpt)
    {
        if (amulets[pos].GetComponent<Slot>().empty)
        {
            amulets[pos].GetComponent<Slot>().UpdateSlotAmulets(pos, ntit, descpt);
        }
    }

    public void UpdateTitDescp(string n, string d)
    {
        title.text = n;
        description.text = d;
    }

    public void ChangeCanvasRight()
    {
        UpdateTitDescp("", "");

        if (invt.activeSelf)
        {
            titleInv.text = "Amuletos";
            dirLeft.text = "Inventario";
            dirRight.text = "Mapa";
            invt.SetActive(false);

            invtAmulets.SetActive(true);
            deco_3.SetActive(true);
            deco_4.SetActive(true);
        }
        else if (map.activeSelf)
        {
            titleInv.text = "Inventario";
            dirLeft.text = "Mapa";
            dirRight.text = "Amuletos";
            map.SetActive(false);

            invt.SetActive(true);
            deco_3.SetActive(true);
            deco_4.SetActive(true);
        }
        else if (invtAmulets.activeSelf)
        {
            titleInv.text = "Mapa";
            dirLeft.text = "Amuletos";
            dirRight.text = "Inventario";
            invtAmulets.SetActive(false);

            map.SetActive(true);
            deco_3.SetActive(false);
            deco_4.SetActive(false);
        }
    }

    public void ChangeCanvasLeft()
    {
        UpdateTitDescp("", "");

        if (invt.activeSelf)
        {
            titleInv.text = "Mapa";
            dirLeft.text = "Amuletos";
            dirRight.text = "Inventario";
            invt.SetActive(false);

            map.SetActive(true);
            deco_3.SetActive(false);
            deco_4.SetActive(false);
        }
        else if (map.activeSelf)
        {
            titleInv.text = "Amuletos";
            dirLeft.text = "Inventario";
            dirRight.text = "Mapa";
            map.SetActive(false);

            invtAmulets.SetActive(true);
            deco_3.SetActive(true);
            deco_4.SetActive(true);
        }
        else if (invtAmulets.activeSelf)
        {
            titleInv.text = "Inventario";
            dirLeft.text = "Mapa";
            dirRight.text = "Amuletos";
            invtAmulets.SetActive(false);

            invt.SetActive(true);
            deco_3.SetActive(true);
            deco_4.SetActive(true);
        }
    }


    public void UpdatePickaxe()
    {
        if (levelPickaxe < 2)
        {
            levelPickaxe++;
            pickaxeInv.GetComponent<Slot>().UpdatePickaxe(levelPickaxe);
            AddGrooves();
        }
    }

    public void AddGrooves()
    {
        if (levelPickaxe < 3)
        {
            GameObject tmp;
            for (int i = 0; i < 3; i++)
            {
                tmp = Instantiate(groovePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                tmp.transform.SetParent(slotsAmlsEq.transform, false);
            }
            
        }
    }

    public void MoveSelector(GameObject go)
    {
        selector.SetActive(true);
        selector.transform.position = go.transform.position;

        selector.GetComponent<RectTransform>().sizeDelta = new Vector2(
            go.GetComponent<RectTransform>().rect.width,
            go.GetComponent<RectTransform>().rect.height);

        /*selector.transform.GetChild(0).position = new Vector3(
            go.GetComponent<RectTransform>().rect.xMin,
            selector.transform.GetChild(0).position.y,
            0);*/
    }

    public void OcultSelector()
    {
        selector.SetActive(false);
    }

    public bool IsObject(int obj)
    {
        Slot s = slots[obj].GetComponent<Slot>();
        if (s.IsEmpty())
            return false;

        return true;
    }

    public bool IsPickaxe()
    {
        return IsObject(0);
    }

    public bool IsLantern()
    {
        return IsObject(1);
    }

    public bool IsUmbrella()
    {
        return IsObject(2);
    }

    public bool IsMap()
    {
        return IsObject(3);
    }

    public bool IsGloves()
    {
        return IsObject(4);
    }

    public bool IsTiara()
    {
        return IsObject(5);
    }

    public bool IsKey(int nkey)
    {
        Slot k = keys[nkey].GetComponent<Slot>();
        if (!k.IsEmpty())
        {
            if (k.GetID() == nkey)
                return true;
        }
            return false;
    }
}
