using System;
using MiniGame;
using UnityEngine;


public class CounterComponent : MonoBehaviour
{
    [SerializeField] int number;

    public void setNumber(int i)
    {
        number = i;
    }

    private void OnMouseDown()
    {
        Debug.Log(CounterGame.getInstance().getCount());
        if (!CounterGame.getInstance().getCanClick())return;
        
        if (CounterGame.getInstance().getCount() != number -1 )
        {
            CounterGame.getInstance().wrongCount();
        }
        else
        {
            CounterGame.getInstance().nextCount();
        }
            
    }
}
