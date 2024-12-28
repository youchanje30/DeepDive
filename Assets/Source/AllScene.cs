using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllScene : MonoBehaviour
{
    public Scenes[] Scenes;
    Scenes curScene;

    void Start()
    {
        SetRandomScene();
    }

    public void SetRandomScene()
    {
        int Randoms = Random.Range(0, Scenes.Length);
        curScene = Scenes[Randoms];
    }


    public void NextScene()
    {
        SetRandomScene();
        curScene.gameObject.SetActive(true);
        //curScene.Begin(go);
    }
    public bool IsWork()
    {
        return curScene.GetEnd();
    }
}
