using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    public AudioClip[] footstepSounds; 
    private AudioSource audioSource;
    public float stepInterval = 0.3f; 
    private float nextStepTime = 0f; 

    private bool nearGrandma = false;
    public float interactRange = 1f;
    public KeyCode interactKey = KeyCode.E;
    private bool inMinigame;
    private MinigameInteractable activeInteractable = null;
    private static PlayerController instance;

    public static PlayerController GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }

    public void SetMinigame(bool f)
    {
        inMinigame = f;
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        inMinigame = false;
    }
    
    
    void Update()
    {
        // E om door dialoog te klikken
        if (DialogueUI.Instance != null && DialogueUI.Instance.IsDialogueRunning)
        {
            if (Input.GetKeyDown(interactKey))
            {
                DialogueUI.Instance.DisplayNextSentence();
            }
            return;
        }
        
        // Beweging
        

        // Interactie met E
        if (Input.GetKeyDown(interactKey))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Dialogue"))
                {
                    hit.GetComponent<DialogueInteractable>()?.Interact();
                    break;
                }

                if (hit.CompareTag("Minigame"))
                {
                    var mini = hit.GetComponent<MinigameInteractable>();
                    if (mini != null)
                    {
                        mini.Interact();
                        activeInteractable = mini;
                        break;
                    }
                }
            }
        }

        // ESC sluit minigame
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activeInteractable != null)
            {
                activeInteractable.CloseMinigame();
                activeInteractable = null;
            }
        }

        if (inMinigame)
        {
            movement.y = 0;
            movement.x = 0;
            return;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetBool("IsMoving", movement.sqrMagnitude > 0.01f);

        if (movement.sqrMagnitude > 0.01f && Time.time > nextStepTime) 
        {
            PlayFootstepSound();
            nextStepTime = Time.time + stepInterval; 
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            // Kies een willekeurig geluid uit de array
            AudioClip footstep = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(footstep);
        }
    }
}
