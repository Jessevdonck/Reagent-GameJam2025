using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    private bool nearGrandma = false;
    public float interactRange = 1f;
    public KeyCode interactKey = KeyCode.E;

    private MinigameInteractable activeInteractable = null;

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);
        animator.SetBool("IsMoving", movement.sqrMagnitude > 0.01f);

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
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }
}