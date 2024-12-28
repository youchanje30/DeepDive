using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AllCutScene : MonoBehaviour
{
    private SceneRunner runner = null;
    
    public CutScenes[] scenes;
    public string[] paths;
    private void Start()
    {
        runner = new SceneRunner();
        var findedStrategy = CreateStrategyFromClassName
            (paths[0]);

        if (findedStrategy != null)
        {
            runner.SetScene(findedStrategy);
            runner.Run();
        }
        else
        {
            Debug.Log("¾ø¾î");
        }

    }

    private void Update()
    {
        runner.Update();
    }
    private CutScenes CreateStrategyFromClassName(string className)
    {
        Type type = Type.GetType(className);

        if (type != null)
        {
            object instance = Activator.CreateInstance(type);

            if (instance is CutScenes myClassInstance)
            {
                return (CutScenes)instance;
            }
        }
        return null;
    }

}
