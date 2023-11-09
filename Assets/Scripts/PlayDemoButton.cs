using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class PlayDemoButton : MonoBehaviour
{
   
    public void LoadDemoDungeon()
    {
        SceneManager.LoadScene("Demo_Dungeon");
    }

    public void OnPlayGame()
    {
        LoadDemoDungeon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
