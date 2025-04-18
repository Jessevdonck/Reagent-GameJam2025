using System;
using UnityEngine;

public class Plunger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private PoopGame game;

    private void Start()
    {
        game = PoopGame.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Minigame"))
        {
            game.PoopHit();
        }
        else if(game.isDipping)
        {
            SoundManager.Instance.PlaySound(game.GetDullSound(), 0.6f);
        }
        
        
        
    }
}
