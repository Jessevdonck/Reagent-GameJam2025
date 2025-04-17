using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Array van voetstappend geluiden
    private AudioSource audioSource;
    public float stepInterval = 0.5f; // Interval tussen voetstappen
    private float nextStepTime = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.time > nextStepTime && IsMoving()) // IsMoving kan een simpele check zijn, bijvoorbeeld gebaseerd op snelheid
        {
            PlayFootstepSound();
            nextStepTime = Time.time + stepInterval;
        }
    }

    void PlayFootstepSound()
    {
        // Kies een willekeurig geluid uit de array
        AudioClip footstep = footstepSounds[Random.Range(0, footstepSounds.Length)];
        audioSource.PlayOneShot(footstep);
    }

    bool IsMoving()
    {
        // Controleer hier of de speler zich verplaatst, bijvoorbeeld door de snelheid te checken.
        return true; // Voor nu stel ik in dat de speler altijd beweegt
    }
}