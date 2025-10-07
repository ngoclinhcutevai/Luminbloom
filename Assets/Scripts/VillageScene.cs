using UnityEngine;
using UnityEngine.UI; 
using System.Collections;
using UnityEngine.SceneManagement;

public class VillageScene : MonoBehaviour
{
    [Header("Next Scene Settings")]
    [SerializeField] private string nextSceneName = "VillageScene";
    
    [Header("UI Reference")]
    [SerializeField] private CanvasGroup promptCanvasGroup; 
    
    [Header("Fading Settings")]
    [SerializeField] private float fadeSpeed = 3f;

    private bool playerIsNearby = false;
    private bool isFading = false;
    private const string PlayerTag = "Player"; 

    private void Start()
    {
        if (promptCanvasGroup != null)
        {
            promptCanvasGroup.alpha = 0f;
        }
    }

    private void Update()
    {
        if (playerIsNearby && promptCanvasGroup.alpha > 0.9f)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Interact();
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
        {
            playerIsNearby = true;
            FadePrompt(1f);
        }
    }

    // When player leaves the trigger
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PlayerTag))
        {
            playerIsNearby = false;
            FadePrompt(0f);
        }
    }

    private void FadePrompt(float targetAlpha)
    {
        if (promptCanvasGroup == null) return;
        
        if (isFading)
        {
            StopAllCoroutines();
            isFading = false;
        }
        StartCoroutine(Co_Fade(targetAlpha));
    }

    private IEnumerator Co_Fade(float targetAlpha)
    {
        isFading = true;
        while (!Mathf.Approximately(promptCanvasGroup.alpha, targetAlpha))
        {
            promptCanvasGroup.alpha = Mathf.MoveTowards(
                promptCanvasGroup.alpha, 
                targetAlpha, 
                fadeSpeed * Time.deltaTime
            );
            yield return null;
        }
        isFading = false;
    }
    
    private void Interact()
    {
        Debug.Log("Loading next scene: " + nextSceneName);
        SceneManager.LoadScene(nextSceneName);
    }
}
