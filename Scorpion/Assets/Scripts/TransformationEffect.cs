using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TransformationEffect : MonoBehaviour
{
    public GameObject player1;               // Player 1 GameObject
    public GameObject player2;               // Player 2 GameObject
    public Sprite monsterSprite;             // The monster sprite to replace the player
    public Camera mainCamera;                // Main camera reference
    public Image screenFlash;                // UI Image covering the screen for the flash effect
    public Text displayText;                 // UI Text to display final words
    public AudioClip musicClip;              // Music clip to play independently
    public float zoomOutAmount = 5f;         // Camera zoom-out target
    public float zoomSpeed = 0.5f;           // Speed of camera zoom-out
    public float fadeDuration = 2f;          // Duration of fade to black
    private bool isEffectTriggered = false;  // To check if the effect has already been triggered

    private SpriteRenderer playerSpriteRenderer1;
    private SpriteRenderer playerSpriteRenderer2;
    private PlayerMovement playerMovement1;
    private PlayerMovement playerMovement2;

    private Rigidbody2D rbPlayer1;
    private Rigidbody2D rbPlayer2;

    private AudioSource audioSource;         // AudioSource to play music

    void Start()
    {
        // Get components for both players
        playerSpriteRenderer1 = player1.GetComponent<SpriteRenderer>();
        playerSpriteRenderer2 = player2.GetComponent<SpriteRenderer>();
        playerMovement1 = player1.GetComponent<PlayerMovement>();
        playerMovement2 = player2.GetComponent<PlayerMovement>();

        // Get Rigidbody2D components for disabling physics-based movement
        rbPlayer1 = player1.GetComponent<Rigidbody2D>();
        rbPlayer2 = player2.GetComponent<Rigidbody2D>();

        // Get the AudioSource component for playing music
        audioSource = gameObject.AddComponent<AudioSource>();

        // Hide the screen flash and text initially
        screenFlash.color = new Color(1, 1, 1, 0); // Transparent white
        displayText.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is either Player1 or Player2
        if (!isEffectTriggered && (other.CompareTag("Player1") || other.CompareTag("Player2")))
        {
            isEffectTriggered = true; // Prevent multiple triggers
            TriggerTransformation();
            // Optionally, destroy the item after it’s picked up
            Destroy(gameObject);
        }
    }

    public void TriggerTransformation()
    {
        // Start the transformation sequence
        StartCoroutine(TransformationSequence());
    }

    private IEnumerator TransformationSequence()
    {
        // Step 1: Disable player movement and Rigidbody2D to stop all physics
        if (playerMovement1 != null)
        {
            playerMovement1.enabled = false;
        }
        if (playerMovement2 != null)
        {
            playerMovement2.enabled = false;
        }

        if (rbPlayer1 != null)
        {
            rbPlayer1.simulated = false; // Disable physics on Player 1
        }
        if (rbPlayer2 != null)
        {
            rbPlayer2.simulated = false; // Disable physics on Player 2
        }

        // Step 2: Pause for 3 seconds
        yield return new WaitForSeconds(3f);

        // Step 3: Flash screen white and change player sprite to monster
        StartCoroutine(ScreenFlashWhite(0.2f));
        playerSpriteRenderer1.sprite = monsterSprite;
        playerSpriteRenderer2.sprite = monsterSprite;

        // Step 4: Camera zoom out and fade to black
        yield return StartCoroutine(CameraZoomAndFade());

        // Step 5: Play the music and show text
        PlayMusic();
        displayText.enabled = true;
    }

    private void PlayMusic()
    {
        // Play the music clip independently
        if (musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.Play();
        }
    }

    private IEnumerator ScreenFlashWhite(float duration)
    {
        screenFlash.color = new Color(1, 1, 1, 1); // Full white
        yield return new WaitForSeconds(duration);
        screenFlash.color = new Color(1, 1, 1, 0); // Transparent white
    }

    private IEnumerator CameraZoomAndFade()
    {
        float initialZoom = mainCamera.orthographicSize;
        float targetZoom = initialZoom + zoomOutAmount;
        float elapsedTime = 0f;

        // Lerp for camera zoom and screen fade to black
        while (elapsedTime < fadeDuration)
        {
            // Zoom out the camera
            mainCamera.orthographicSize = Mathf.Lerp(initialZoom, targetZoom, elapsedTime / fadeDuration);

            // Fade screen to black
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            screenFlash.color = new Color(0, 0, 0, alpha); // Black fade

            elapsedTime += Time.deltaTime * zoomSpeed;
            yield return null;
        }

        // Ensure final black screen after loop
        screenFlash.color = new Color(0, 0, 0, 1);
    }
}
