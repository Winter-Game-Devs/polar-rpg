using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipRefsSO audioClipRefsSO; // Scriptable object with audio clips
    public Player player;
    public Igloo igloo;
    private AudioSource audioSource;

    private enum State
    {
        Idle,
        Walking,
        Digging,
        Fishing,
        Attacked
    }

    private State currentState;
    private State previousState;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing!");
        }

        currentState = State.Idle;
        previousState = State.Idle;
    }

    private void Update()
    {
        // Determine the current state based on animations and villain actions
        if (player.animator.GetBool("IsMoving")) currentState = State.Walking;
        else if (player.animator.GetBool("isFishing")) currentState = State.Fishing;
        else if (player.animator.GetBool("isDigging")) currentState = State.Digging;
        else if (igloo.isBeingRobbed) currentState = State.Attacked;
        else currentState = State.Idle;

        // Handle state change
        if (currentState != previousState)
        {
            HandleStateChange(currentState);
            previousState = currentState;
        }

        Debug.Log(currentState);
    }

    private void HandleStateChange(State newState)
    {
        switch (newState)
        {
            case State.Idle:
                StopAudio();
                break;
            case State.Walking:
                PlayAudio(audioClipRefsSO.moving);
                break;
            case State.Fishing:
                PlayAudio(audioClipRefsSO.fishing);
                break;
            case State.Digging:
                PlayAudio(audioClipRefsSO.digging);
                break;
            case State.Attacked:
                PlayAudio(audioClipRefsSO.thiefing);
                break;
        }
    }

    private void PlayAudio(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

    private void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
