using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    
    int ID;
    string nameObj;
    string description;
    public bool empty;
    Dictionary<int, int> keys, objs, pickLevel, pickSizeX, pickSizeY;

    GameController gameCont;
    Inventory inv;
    Selectable selector;

    private void Awake()
    {
        keys = new Dictionary<int, int>();
        keys.Add(0, 10);
        keys.Add(1, 11);
        keys.Add(2, 18);
        keys.Add(3, 17);

        objs = new Dictionary<int, int>();
        objs.Add(0, 16);
        objs.Add(1, 19);
        objs.Add(2, 20);
        objs.Add(3, 15);
        objs.Add(4, 12);
        objs.Add(5, 21);

        pickLevel = new Dictionary<int, int>();
        pickLevel.Add(0, 16);
        pickLevel.Add(1, 26);
        pickLevel.Add(2, 25);

        pickSizeX = new Dictionary<int, int>();
        pickSizeX.Add(0, 16);
        pickSizeX.Add(1, 26);
        pickSizeX.Add(2, 25);

        pickSizeY = new Dictionary<int, int>();
        pickSizeY.Add(0, 16);
        pickSizeY.Add(1, 26);
        pickSizeY.Add(2, 25);

        empty = true;
        gameCont = GameController.Instance;
        inv = FindObjectOfType<Inventory>();
        selector = GetComponent<Selectable>();
    }
    void Start()
    {
    }

    public int GetID()
    {
        return ID;
    }
    public void SetID(int newID)
    {
        ID = newID;
    }

    public string GetName()
    {
        return nameObj;
    }
    public void SetName(string newN)
    {
        nameObj = newN;
    }

    public string GetDescription()
    {
        return description;
    }
    public void SetDescription(string newD)
    {
        description = newD;
    }

    public bool IsEmpty()
    {
        return empty;
    }

    public void UpdateSlotAmulets(int pos, string ntit, string descpt)
    {
        empty = false;
        nameObj = ntit;
        description = descpt;

        Image imgSp = GetComponent<Image>();
        // No necesita diccionario, pos coincide con la posicion del sprite
        imgSp.sprite = gameCont.itemsSprites[pos];

        ActivateSelectable();
    }

    public void UpdateSlotKeys(int pos, string ntit, string descpt)
    {
        empty = false;
        nameObj = ntit;
        description = descpt;

        Image imgSp = GetComponent<Image>();
        imgSp.sprite = gameCont.itemsSprites[keys[pos]];
        
        ActivateSelectable();
    }

    public void UpdateSlotInvt(int pos, string ntit, string descpt)
    {
        empty = false;
        nameObj = ntit;
        description = descpt;

        Image imgSp = GetComponent<Image>();
        imgSp.sprite = gameCont.itemsSprites[objs[pos]];
        RectTransform rectTrans = GetComponent<RectTransform>();
        switch (pos)
        {
            case 1: // linterna
                rectTrans.sizeDelta = new Vector2(60, 150);
                break;
            case 2: // paraguas
                rectTrans.sizeDelta = new Vector2(60, 150);
                break;
            case 3: // mapa
                rectTrans.sizeDelta = new Vector2(90, 50);
                break;
            case 4: // guantes
                rectTrans.sizeDelta = new Vector2(90, 100);
                break;
            case 5: // tiara
                rectTrans.sizeDelta = new Vector2(150, 100);
                break;
            default:// 0, pico
                rectTrans.sizeDelta = new Vector2(90, 100);
                break;
        }
        ActivateSelectable();
    }

    public void UpdatePickaxe(int level)
    {
        //empty = false;

        Image imgSp = GetComponent<Image>();
        imgSp.sprite = gameCont.itemsSprites[pickLevel[level]];
        RectTransform rectTrans = GetComponent<RectTransform>();
        rectTrans.localScale = new Vector3(2.5f, 2.5f, 0);
    }

        public void ActivateSelectable()
    {
        selector.enabled = true;
    }

    public void SelectedSlot()
    {
        if (!empty)
        {
            inv.UpdateTitDescp(nameObj, description);
            inv.MoveSelector(this.gameObject);
        }
    }

    public void ClearTitDescp()
    {
        inv.UpdateTitDescp("", "");
        inv.OcultSelector();
    }

    
}
