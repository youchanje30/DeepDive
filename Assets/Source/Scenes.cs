using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenes : MonoBehaviour
{
    public bool isEnd;
    public Image img;
    GameObject curscene;
    public void Start()
    {
        img = GetComponent<Image>();
    }

    public void Begin(GameObject go)
    {
        go = curscene;
    }
    public bool GetEnd()
    {
        return isEnd;
    }
}
