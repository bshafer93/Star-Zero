using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AREA_NAME: int { RM1, RM2, RM3, RM4, RM5, RM6, RM7, RM8, H1,H2,H3 };

public class DungeonManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public GameObject Room01_Container;

    [SerializeField]
    public GameObject Room02_Container;

    [SerializeField]
    public GameObject Room03_Container;

    [SerializeField]
    public GameObject Room04_Container;

    [SerializeField]
    public GameObject Room05_Container;

    [SerializeField]
    public GameObject Room06_Container;

    [SerializeField]
    public GameObject Room07_Container;

    [SerializeField]
    public GameObject Room08_Container;

    [SerializeField]
    public GameObject Hall01_Container;

    [SerializeField]
    public GameObject Hall02_Container;

    [SerializeField]
    public GameObject Hall03_Container;

    [SerializeField]
    public GameObject Door_1T2;

    [SerializeField]
    public GameObject Door_2TH1;

    [SerializeField]
    public GameObject Door_H1T4;

    [SerializeField]
    public GameObject Door_2T3;

    [SerializeField]
    public GameObject Door_3TH2;

    [SerializeField]
    public GameObject Door_H2T5;

    [SerializeField]
    public GameObject Door_H3T7;

    [SerializeField]
    public GameObject Door_R5TH3;

    [SerializeField]
    public GameObject Door_Small_Open_Door;

    [SerializeField]
    public GameObject Door_Boss;

    [SerializeField]
    public GameObject[] Doors_List;

    public static GameObject[] enemy_containers = new GameObject[11];

    [SerializeField]
    public GameObject Boss_Door_Puzzle_Object;

    public static GameObject Boss_Door_Static;

    void Awake()
    {
        

    }

    void Start()
    {
        enemy_containers[(int)AREA_NAME.RM1] = getEnemyContainer(Room01_Container);
        enemy_containers[(int)AREA_NAME.RM2] = getEnemyContainer(Room02_Container);
        enemy_containers[(int)AREA_NAME.RM3] = getEnemyContainer(Room03_Container);
        enemy_containers[(int)AREA_NAME.RM4] = getEnemyContainer(Room04_Container);
        enemy_containers[(int)AREA_NAME.RM5] = getEnemyContainer(Room05_Container);
        enemy_containers[(int)AREA_NAME.RM6] = getEnemyContainer(Room06_Container);
        enemy_containers[(int)AREA_NAME.RM7] = getEnemyContainer(Room07_Container);
        enemy_containers[(int)AREA_NAME.RM8] = getEnemyContainer(Room08_Container);
        enemy_containers[(int)AREA_NAME.H1] = getEnemyContainer(Hall01_Container);
        enemy_containers[(int)AREA_NAME.H2] = getEnemyContainer(Hall02_Container);
        enemy_containers[(int)AREA_NAME.H3] = getEnemyContainer(Hall03_Container);
        Boss_Door_Static = Door_Boss;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


     GameObject getEnemyContainer(GameObject area) {

        Transform[] all_children = area.GetComponentsInChildren<Transform>();
        
        for (int i = 0; i < all_children.Length; i++ ) {
            if(all_children[i].gameObject.tag == "EnemyContainer")
            {
                return all_children[i].gameObject;
            }
        }
        Debug.Log("For some reason enemy container not found for" + area.name);
        return null;
    }

    public void RevealArea(string name)
    {

        switch (name)
        {
            case "R1":
                Room01_Container.SetActive(true);
                break;
            case "R2":
                Room02_Container.SetActive(true);
                Door_2T3.SetActive(true);
                Door_2TH1.SetActive(true);
                break;
            case "R3":
                Room03_Container.SetActive(true);
                Door_3TH2.SetActive(true);
                break;
            case "R4":
                Room04_Container.SetActive(true);
                break;
            case "R5":
                Room05_Container.SetActive(true);
                Door_R5TH3.SetActive(true);
                Door_Boss.SetActive(true);
                Door_Small_Open_Door.SetActive(true);
                Room06_Container.SetActive(true);
                break;
            case "R7":
                Room07_Container.SetActive(true);
                break;
            case "R8":
                Room08_Container.SetActive(true);
                break;
            case "H1":
                Hall01_Container.SetActive(true);
                Door_H1T4.SetActive(true);
                break;
            case "H2":
                Hall02_Container.SetActive(true);
                Door_H2T5.SetActive(true);
                break;
            case "H3":
                Hall03_Container.SetActive(true);
                Door_H3T7.SetActive(true);
                break;
            default:
                Debug.Log("Room Name Invalid");
                break;
        }


    }
}
