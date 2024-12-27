using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTone<GameManager>
{
    private bool is_night;

    public void ChangeTime()
    {
        is_night = !is_night;

        if (is_night)
            OpenShop();
        else
            OpenSell();




    }

    public void OpenSell()
    {
        /*
        ChangeView()
        
        Come_Hero()

        ChangeTime()
        */
    }

    public void OpenShop()
    {
        /*
        ChangeView()
        select_materials 3
        buy_selected_materials
        ChangTime()
        */
    }


    // void Update()
    // {
        
    // }
}
