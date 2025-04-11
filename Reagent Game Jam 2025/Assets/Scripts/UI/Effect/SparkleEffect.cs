using UnityEngine;
using UnityEngine.UI;

public class SparkleEffect : MonoBehaviour
{
    private Image uiImage;           
    private float targetAlpha;         
    private Vector3 targetScale;      
    private Color targetColor;       

    public float sparkleSpeed = 5f;   
    public float minAlpha = 0f;      
    public float maxAlpha = 1f;     
    public float minScale = 0.8f;    
    public float maxScale = 1.5f;     
    public Color glowColor = Color.white; 

    void Start()
    {
        uiImage = GetComponent<Image>();

        targetAlpha = Random.Range(minAlpha, maxAlpha);
        
        targetScale = new Vector3(Random.Range(minScale, maxScale), Random.Range(minScale, maxScale), 1f);
        
        targetColor = glowColor;
    }

    void Update()
    {

        float scaleFactor = Mathf.PingPong(Time.time * sparkleSpeed, maxScale - minScale) + minScale;
        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);  

 
        Color currentColor = uiImage.color;
        currentColor.a = Mathf.Lerp(currentColor.a, targetAlpha, Time.deltaTime * sparkleSpeed); 
        uiImage.color = currentColor;

        if (Mathf.Abs(currentColor.a - targetAlpha) < 0.05f)
        {
            targetAlpha = Random.Range(minAlpha, maxAlpha);
        }
    }
}