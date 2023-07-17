using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float stepDistance = 1f;   // Distance covered in each step
    public float moveSpeed = 2f;     // Speed of movement
    public Vector3 moveAxis = Vector3.forward;   // Axis along which the object moves
    public float bounceHeight = 0.1f; // Maximum height of the bounce effect
    public float bounceFrequency = 2f; // Frequency of the bounce effect
    public float bounceRandomness = 0.05f; // Randomness added to each bounce
    public AudioClip[] stepSounds1; // Array of step sound clips
    public AudioClip[] stepSounds2; // Array of step sound clips
    public AudioClip[] stepSounds3; // Array of step sound clips
    public AudioClip[] stepSounds4; // Array of step sound clips



    private float currentStepDistance;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;
    private float timeOffset = 0f;
    public AudioSource audioSource;


    private void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + moveAxis * stepDistance;
        isMoving = true;
        timeOffset = Time.time;
    }

    private void Update()
    {
        if (isMoving)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (transform.position == targetPosition)
            {
                isMoving = false;

                targetPosition += moveAxis * stepDistance;
                isMoving = true;
                timeOffset = Time.time;

                PlayStepSound();
            }
        }

        if (isMoving)
        {
            // Calculate the bounce effect with randomness
            float y = startPosition.y + Mathf.Sin((Time.time - timeOffset) * bounceFrequency) * bounceHeight;
            y += Random.Range(-bounceRandomness, bounceRandomness);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
    }

    private void PlayStepSound()
    {
        float currentPosition = transform.position.x;
        if (stepSounds1.Length > 0 && currentPosition < 50.2)
        {
            int randomIndex = Random.Range(0, stepSounds1.Length-1);
            audioSource.PlayOneShot(stepSounds1[randomIndex]);
        }
        if (stepSounds2.Length > 0 && currentPosition >= 50.2 && currentPosition < 100.4)
        {
            int randomIndex = Random.Range(0, stepSounds2.Length - 1);
            audioSource.PlayOneShot(stepSounds2[randomIndex]);
        }
        if (stepSounds3.Length > 0 && currentPosition >= 100.4 && currentPosition < 150.6)
        {
            int randomIndex = Random.Range(0, stepSounds3.Length - 1);
            audioSource.PlayOneShot(stepSounds3[randomIndex]);
        }
        if (stepSounds4.Length > 0 && currentPosition >= 150.6)
        {
            int randomIndex = Random.Range(0, stepSounds4.Length - 1);
            audioSource.PlayOneShot(stepSounds4[randomIndex]);
        }
    }
}
