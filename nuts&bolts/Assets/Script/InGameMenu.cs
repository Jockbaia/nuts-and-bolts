using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    public void OnResume()
    {
        PlayerLogic.menuOpen = false;
        PlayerLogic.menuCanvas.SetActive(false);
    }
}
