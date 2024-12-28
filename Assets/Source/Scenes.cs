using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenes : MonoBehaviour
{
    public bool isEnd;
    public Image img;
    public void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool GetEnd()
    {
        return isEnd;
    }
}
