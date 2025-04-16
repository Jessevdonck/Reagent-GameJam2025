using System;
using System.Collections;
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
        if (!CounterGame.getInstance().getCanClick()) return;

        StartCoroutine(ButtonPressEffect());
    
        if (CounterGame.getInstance().getCount() != number - 1)
        {
            CounterGame.getInstance().wrongCount();
        }
        else
        {
            CounterGame.getInstance().nextCount();
        }
    }

    IEnumerator ButtonPressEffect()
    {
        Vector3 pos = transform.position;
        pos.y -= 0.1f;
        transform.position = pos;

        yield return new WaitForSeconds(0.1f);

        pos.y += 0.1f;
        transform.position = pos;
    }
    
    
}
