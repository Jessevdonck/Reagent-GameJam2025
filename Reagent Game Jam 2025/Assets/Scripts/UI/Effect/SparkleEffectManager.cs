using UnityEngine;
using UnityEngine.UI;

public class SparkleEffectManager : MonoBehaviour
{
    public GameObject sparklePrefab;     
    public Canvas canvas;               
    public int numberOfSparkles = 50;  

    void Start()
    {
        for (int i = 0; i < numberOfSparkles; i++)
        {
            CreateSparkle();
        }
    }

    void CreateSparkle()
    {
        GameObject sparkle = Instantiate(sparklePrefab);

        sparkle.transform.SetParent(canvas.transform, false); 

        float randomX = Random.Range(-960f, 960f);  
        float randomY = Random.Range(-540f, 540f);  
        sparkle.transform.localPosition = new Vector2(randomX, randomY);

        sparkle.transform.SetAsFirstSibling(); 
    }
}