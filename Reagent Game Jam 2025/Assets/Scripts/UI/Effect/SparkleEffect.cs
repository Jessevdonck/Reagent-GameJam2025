using UnityEngine;
using UnityEngine.UI;

public class SparkleEffect : MonoBehaviour
{
    public float sparkleSpeed = 5f;
    public float minAlpha;
    public float maxAlpha = 1f;
    public float minScale = 0.8f;
    public float maxScale = 1.5f;
    public Color glowColor = Color.white;
    private float targetAlpha;
    private Color targetColor;
    private Vector3 targetScale;
    private Image uiImage;

    private void Start()
    {
        uiImage = GetComponent<Image>();

        targetAlpha = Random.Range(minAlpha, maxAlpha);

        targetScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), 1f);

        targetColor = glowColor;
    }

    private void Update()
    {
        var scaleFactor = Mathf.PingPong(Time.time * sparkleSpeed, maxScale - minScale) + minScale;
        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);


        var currentColor = uiImage.color;
        currentColor.a = Mathf.Lerp(currentColor.a, targetAlpha, Time.deltaTime * sparkleSpeed);
        uiImage.color = currentColor;

        if (Mathf.Abs(currentColor.a - targetAlpha) < 0.05f) targetAlpha = Random.Range(minAlpha, maxAlpha);
    }
}