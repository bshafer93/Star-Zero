using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{

    static public int accessKeys = 0;
    [SerializeField] public GameObject[] instruments = new GameObject[2];
    [SerializeField] public int currentInstrumentIndex = -1;
    static public bool hasThunderGuitar = false;
    static public bool hasRythym = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Sets player instrument(weapon) based on index.
    /// TODO -- more error checking logic.
    /// </summary>
    ///<param name="index">Index of the weapon to switch too.</param>
    public  void changeInstrument(int index)
    {

        //Player has not gotten that weapon yet. 
        if (index == 1 && hasThunderGuitar == false)
        {
            Debug.Log("Item has not been obtained!");
            return;
        }

        if (currentInstrumentIndex == index)
        {
            if (currentInstrumentIndex != -1)
            {
                if (instruments[currentInstrumentIndex].activeInHierarchy)
                {
                    instruments[currentInstrumentIndex].SetActive(false);
                    currentInstrumentIndex = -1;
                }
            }

        }
        else if (currentInstrumentIndex != index)
        {
            //MAKE SURE TO DO ERROR CHECK WHEN MORE INSTRUMENTS GET IMPLEMENTED
            if (currentInstrumentIndex > -1)
            {
                instruments[currentInstrumentIndex].SetActive(false);
            }
            currentInstrumentIndex = index;
            instruments[currentInstrumentIndex].SetActive(true);
        }


    }

    public  Instrument getCurrentInstrument() {

        if (currentInstrumentIndex == -1) {
            return null;
        }
       return instruments[currentInstrumentIndex].GetComponent<Instrument>();
    }


}
