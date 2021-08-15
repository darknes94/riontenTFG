using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public Sprite[] itemsSprites;
    public GameObject mainCamera;

    // Prefabs
    GameObject keyPrefab, amuletPrefab, moneyPrefab, pickaxePrefab,
        lanternPrefab, umbrellaPrefab, mapPrefab, glovePrefav, tiaraPrefab,
        hub, slotLife;

    // Listas
    List<GameObject> keys, moneys, amulets, items, slotLifes;

    // Valores de las listas
    Dictionary<int, string> nameKeysDict, descpKeysDict,
        nameAmuDict, descpAmuDict, nameItemDict, descpItemDict;
    Dictionary<int, Vector3> colorAmuDict;
    Dictionary<int, GameObject> prefabDict;

    //Dictionary<int, Vector2>;

    // Variables game
    int money, lifes, maxLifes, lastLife, totalMaxLifes;

    GameObject lifesSlots;


    bool shieldEnabled;


    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        itemsSprites = Resources.LoadAll<Sprite>("Sprites/Objects/items");

        keyPrefab = Resources.Load<GameObject>("Prefabs/key");
        amuletPrefab = Resources.Load<GameObject>("Prefabs/amulet");
        moneyPrefab = Resources.Load<GameObject>("Prefabs/moneyPrefab");
        pickaxePrefab = Resources.Load<GameObject>("Prefabs/pickaxe");
        lanternPrefab = Resources.Load<GameObject>("Prefabs/lantern");
        umbrellaPrefab = Resources.Load<GameObject>("Prefabs/umbrella");
        mapPrefab = Resources.Load<GameObject>("Prefabs/objMap");
        glovePrefav = Resources.Load<GameObject>("Prefabs/glove");
        tiaraPrefab = Resources.Load<GameObject>("Prefabs/tiara");
        slotLife = Resources.Load<GameObject>("Prefabs/slotLife");
        //hubPrefab = Resources.Load<GameObject>("Prefabs/hub");

        //mainCamera = transform.Find("mainCamera").gameObject;
        hub = mainCamera.transform.GetChild(0).gameObject;
        lifesSlots = hub.transform.Find("lifesSlots").gameObject;

        CreateMoneys();
        CreateKeys();
        CreateAmulets();
        CreateObjects();

        InitializeHub();
    }

    private void InitializeHub()
    {
        shieldEnabled = false;
        maxLifes = 8;
        lifes = 4;
        totalMaxLifes = lifes;
        lastLife = lifes - 1;

        slotLifes = new List<GameObject>();
        

        // Desactivamos las 4 vidas de abajo del hub
        for (int i = lifes; i < maxLifes; i++)
        {
            //slotLifes.Add();

            // Escudo y vida desactivados
            lifesSlots.transform.GetChild(i).transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            lifesSlots.transform.GetChild(i).transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            // Slot de vida desactivado
            lifesSlots.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void AddMoney()
    {
        money += 10;
    }

    public int GetMoney()
    {
        return money;
    }

    public void CreateMoneys()
    {
        money = 0;

        moneys = new List<GameObject>();
        GameObject mAux = null;
        for (int i = 0; i < 5; i++)
        {
            mAux = Instantiate(moneyPrefab, new Vector3(-8-i, 0.5f, 0), Quaternion.identity);
            moneys.Add(mAux);
        }
        mAux = null;
    }

    public void CreateKeys()
    {
        nameKeysDict = new Dictionary<int, string>()
        {
            { 0, "Llave vida" },
            { 1, "Llave codicia"},
            { 2, "Llave luz"},
            { 3, "Llave verdad"}
        };

        descpKeysDict = new Dictionary<int, string>()
        {
            { 0, "Abre la puerta de la vida." },
            { 1, "Abre la puerta de la codicia."},
            { 2, "Abre la puerta de la luz"},
            { 3, "Abre la puerta de la verdad"}
        };

        keys = new List<GameObject>();
        GameObject keyAux = null;
        for (int i = 0; i < 4; i++)
        {
            keyAux = Instantiate(keyPrefab, new Vector3(i, 0, 0), Quaternion.Euler(0, 0, -35));
            keyAux.GetComponent<Key>().SetID(i);
            keyAux.GetComponent<Key>().SetName(nameKeysDict[i]);
            keyAux.GetComponent<Key>().SetDescription(descpKeysDict[i]);
            keys.Add(keyAux);
        }
        keyAux = null;
    }

    public void CreateAmulets()
    {
        nameAmuDict = new Dictionary<int, string>()
        {
            { 0, "Cura"},
            { 1, "Vida"},
            { 2, "Renacer"},
            { 3, "Magia"},
            { 4, "Escudo"},
            { 5, "Imán"},
            { 6, "Invisible"},
            { 7, "- Maná"},
            { 8, "Codicia"}
        };

        descpAmuDict = new Dictionary<int, string>()
        {
            { 0, "Amuleto cura"},
            { 1, "Amuleto vida"},
            { 2, "Amuleto renacer"},
            { 3, "Amuleto magia"},
            { 4, "Amuleto escudo"},
            { 5, "Amuleto que atrae a las perlas."},
            { 6, "Amuleto que te hace invisible para los enemigos."},
            { 7, "Amuleto que reduce el coste de maná."},
            { 8, "Amuleto que duplica las perlas recogidas."}
        };

        colorAmuDict = new Dictionary<int, Vector3>()
        {
            { 0, new Vector3(0, 255, 0)},
            { 1, new Vector3(0, 145, 0)},
            { 2, new Vector3(255, 70, 130)},
            { 3, new Vector3(0, 210, 210)},
            { 4, new Vector3(255, 95, 0)},
            { 5, new Vector3(220, 0, 0)},
            { 6, new Vector3(135, 75, 255)},
            { 7, new Vector3(40, 110, 215)},
            { 8, new Vector3(255, 195, 0)}
        };

        amulets = new List<GameObject>();
        GameObject amuAux = null;
        for (int i = 0; i < 9; i++)
        {
            amuAux = Instantiate(amuletPrefab, new Vector3(-5-i, -2, 0), Quaternion.Euler(0, 0, -35));
            amuAux.GetComponent<Amulet>().SetID(i);
            amuAux.GetComponent<Amulet>().SetName(nameAmuDict[i]);
            amuAux.GetComponent<Amulet>().SetDescription(descpAmuDict[i]);
            amuAux.GetComponent<Amulet>().SetColor(colorAmuDict[i]);
            amulets.Add(amuAux);
        }
        amuAux = null;
    }

    public void CreateObjects()
    {
        prefabDict = new Dictionary<int, GameObject>()
        {
            { 0, pickaxePrefab },
            { 1, lanternPrefab},
            { 2, umbrellaPrefab},
            { 3, mapPrefab},
            { 4, glovePrefav},
            { 5, tiaraPrefab}
        };

        nameItemDict = new Dictionary<int, string>()
        {
            { 0, "Pico" },
            { 1, "Linterna"},
            { 2, "Paraguas"},
            { 3, "Mapa"},
            { 4, "Guantes"},
            { 5, "Tiara de maná"}
        };

        descpItemDict = new Dictionary<int, string>()
        {
            { 0, "Un objeto que se dejaron olvidado los antiguos. ¿Quién sabe?, podría servir de arma." },
            { 1, "Objeto que funciona con una especie de baba luminiscente que se evapora. Utiliza con cabeza o busca al xxxx en las profundidades."},
            { 2, "Objeto hecho con la falda de una preciosa bailarina. Te permitirá elevarte en los geiseres y planear."},
            { 3, "Un simple mapa."},
            { 4, "Objeto que te permite escalar paredes."},
            { 5, "Objeto que te permite recolectar maná del ambiente."}
        };

        items = new List<GameObject>();
        GameObject itemAux = null;
        for (int i = 0; i < 6; i++)
        {
            itemAux = Instantiate(prefabDict[i]);
            itemAux.GetComponent<Items>().SetID(i);
            itemAux.GetComponent<Items>().SetName(nameItemDict[i]);
            itemAux.GetComponent<Items>().SetDescription(descpItemDict[i]);
            items.Add(itemAux);
        }
        itemAux = null;
    }

    public void EraseLife()
    {
        // if (!shieldEnabled)
        if (lifes == 0)
        {
            // Muerto o renacer
        } 
        else
        {
            lifesSlots.transform.GetChild(lastLife).GetComponent<SpriteRenderer>().enabled = false;

            lifes--;
            lastLife = lifes - 1;
        }
    }

    public void ActivateAllLifes()
    {
        totalMaxLifes = maxLifes - lifes;

        //lastLife = maxLifes - 1;

        for (int i = 4; i < maxLifes; i++)
        {
            // Slot de vida activado
            lifesSlots.transform.GetChild(i).gameObject.SetActive(true);
        }

        // Escudo activado si amuleto esta enabled
        //lifesSlots.transform.GetChild(i).transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ActivateAllLifes();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            EraseLife();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ActivatedShield();
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            DesactivatedShield();
        }
    }

    public void ActivatedShield()
    {
        shieldEnabled = true;
    }

    public void DesactivatedShield()
    {
        shieldEnabled = false;
    }

    public void OpenDoorLever(int codeDoor) 
    {
        DoorLever door = FindObjectOfType<DoorLever>();
        door.Open();
    }

    public void CloseDoorLever(int codeDoor)
    {
        DoorLever door = FindObjectOfType<DoorLever>();
        door.Close();
    }
}
