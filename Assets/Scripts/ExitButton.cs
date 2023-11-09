using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ExitButton : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void ExitToDesktop() {
        Application.Quit();
    }

    public void OnPlayGame() {
        ExitToDesktop();
    
    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
