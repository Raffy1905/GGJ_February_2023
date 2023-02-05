using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            Debug.Log("1");
            ThemeManager.instance.NextTheme();
        }
    }
}
