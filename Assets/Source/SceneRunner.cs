using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneRunner 
{
    private CutScenes RunScene = null;

    public void Run()
    {
        if (RunScene == null) return;
        RunScene.Run();
    }
    public void SetScene(CutScenes Scenes)
    {
        this.RunScene = Scenes;
    }
    public CutScenes GetScene()
    {
        return this.RunScene;
    }
    public void Update()
    {
        if (RunScene != null)
        {
            RunScene.Update();
        }
    }
}
