using UnityEngine;
using System.Collections;
public class TouchResponsiveFlora : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color glowColor = new Color(0.5f, 1f, 0.5f, 1f); // Soft green glow
    private Vector3 originalScale;
    private bool isGlowing = false;

    public float glowSpeed = 2f;
    public float touchScaleFactor = 1.2f; // Size increase on touch
    public float resetSpeed = 2f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (isGlowing)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, glowColor, Time.deltaTime * glowSpeed);
        }
        else
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, originalColor, Time.deltaTime * glowSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isGlowing = true;
            StopAllCoroutines();
            StartCoroutine(TouchEffect());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isGlowing = false;
            StopAllCoroutines();
            StartCoroutine(ResetEffect());
        }
    }

    private IEnumerator TouchEffect()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 0.2f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * touchScaleFactor, elapsedTime * 5);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ResetEffect()
    {
        float elapsedTime = 0f;
        while (elapsedTime < 0.5f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, elapsedTime * resetSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
