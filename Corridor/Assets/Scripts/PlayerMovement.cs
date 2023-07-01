using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float stepDistance = 1f;   // Distance covered in each step
    public float moveSpeed = 2f;     // Speed of movement
    public Vector3 moveAxis = Vector3.forward;   // Axis along which the object moves
    public float bounceHeight = 0.1f; // Maximum height of the bounce effect
    public float bounceFrequency = 2f; // Frequency of the bounce effect
    public float bounceRandomness = 0.05f; // Randomness added to each bounce
    public AudioClip[] stepSounds; // Array of step sound clips

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
        if (stepSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, stepSounds.Length-1);
            audioSource.PlayOneShot(stepSounds[randomIndex]);
        }
    }
}
