using UnityEngine;
using UnityEngine.InputSystem;

public class ApplicationQuit : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current.escapeKey.isPressed) {
            Debug.Log("Pressed Escape Key");
            Application.Quit();

        }
        
    }
}
