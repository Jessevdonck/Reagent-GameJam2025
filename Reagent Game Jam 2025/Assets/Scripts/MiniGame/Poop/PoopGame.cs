using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PoopGame : MonoBehaviour, IMinigame
{
    [SerializeField] private GameObject plunger;
    [SerializeField] private List<GameObject> poops;
    [SerializeField] private float plungerSpeed = 2f;
    [SerializeField] private float plungerDipAmount = 0.5f;
    [SerializeField] private float movementRange = 3f;
    
    [SerializeField] private List<AudioClip> goopSounds;
    [SerializeField] private AudioClip dullSound;

    private static PoopGame instance;
    public MinigameInteractable interactableParent;
    public static PoopGame GetInstance()
    {
        return instance;
    }
    
    
    
    private int hitCount;
    private int hitLimit;
    private bool isGameCompleted;
    public bool isDipping;

    private Vector3 plungerStartPos;
    private bool movingRight = true;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
        hitCount = 0;
        hitLimit = poops.Count;
        isGameCompleted = false;
        plungerStartPos = plunger.transform.position;
        
        
    }

    private void Update()
    {
        if (isGameCompleted) return;

        // Handle plunger movement
        if (!isDipping)
        {
            MovePlunger();
        }

        // Input
        if (Input.GetKeyDown(KeyCode.Space) && !isDipping)
        {
            StartCoroutine(UsePlunger());
        }
    }

    void MovePlunger()
    {
        float step = (plungerSpeed + Random.Range(-5f,10f)) * Time.deltaTime ;
        Vector3 pos = plunger.transform.position;

        if (movingRight)
        {
            pos.x += step;
            if (pos.x > plungerStartPos.x + movementRange)
            {
                pos.x = plungerStartPos.x + movementRange;
                movingRight = false;
            }
        }
        else
        {
            pos.x -= step;
            if (pos.x < plungerStartPos.x - movementRange)
            {
                pos.x = plungerStartPos.x - movementRange;
                movingRight = true;
            }
        }

        plunger.transform.position = pos;
    }

    

    IEnumerator UsePlunger()
    {
        isDipping = true;

        Vector3 originalPos = plunger.transform.position;
        Vector3 dippedPos = originalPos - new Vector3(0, plungerDipAmount, 0);

        // Dip down
        plunger.transform.position = dippedPos;
        yield return new WaitForSeconds(0.3f);

        // Return to original height
        plunger.transform.position = originalPos;
        yield return new WaitForSeconds(0.2f);

        isDipping = false;

        
       
    }

    public void PoopHit()
    {
        Debug.Log("poophit");
        PlayRandomGoopSound();
        if (hitCount < hitLimit)
        {
            Destroy(poops[hitCount]);
            hitCount++;
        }

        if (hitCount == hitLimit)
        {
            GameCompleted();
        }
    }
    
    
    private void GameCompleted()
    {
        LevelManager.Instance.MarkMinigameComplete(4);

        if (interactableParent != null)
        {
            interactableParent.OnMinigameCompleted();
        }
        
        StartCoroutine(EndMinigame());
    }
    
    public void SetParentInteractable(MinigameInteractable interactable)
    {
        interactableParent = interactable;
    }
    
    void PlayRandomGoopSound()
    {
        if (goopSounds.Count > 0)
        {
            AudioClip clip = goopSounds[Random.Range(0, goopSounds.Count)];
            SoundManager.Instance.PlaySound(clip, 0.6f);
        }
    }
    
    public AudioClip GetDullSound()
    {
        return dullSound;
    }
    

    public void SelfDestruct()
    {
        Destroy(this);
    }

    private IEnumerator EndMinigame()
    {
        yield return new WaitForSeconds(2f);
        StopAllCoroutines();
        this.gameObject.SetActive(false);
    }
}
