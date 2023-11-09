using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuManager : MonoBehaviour
{
    public PlayerInputAsset controls;

    void Start()
    {
        controls = new PlayerInputAsset();
        controls.Player.StartGame.started += (context)=>{ SceneManager.LoadScene("Demo_Dungeon"); };
        controls.Player.ExitGame.started += (context) => { Application.Quit(); };

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
