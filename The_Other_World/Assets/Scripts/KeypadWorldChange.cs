using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadWorldChange : MonoBehaviour
{
    public GameObject BG;
    public GameObject BG2;
    // Start is called before the first frame update
    void Start()
    {
        BG.SetActive(WorldSwitch.isWorld1);
        BG2.SetActive(!WorldSwitch.isWorld1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            WorldSwitch.isWorld1 = !WorldSwitch.isWorld1;
            BG.SetActive(WorldSwitch.isWorld1);
            BG2.SetActive(!WorldSwitch.isWorld1);
        }
    }
}
