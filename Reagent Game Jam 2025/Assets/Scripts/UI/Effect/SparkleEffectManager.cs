using UnityEngine;

public class SparkleEffectManager : MonoBehaviour
{
    public GameObject sparklePrefab;
    public Canvas canvas;
    public int numberOfSparkles = 50;

    private void Start()
    {
        for (var i = 0; i < numberOfSparkles; i++) CreateSparkle();
    }

    private void CreateSparkle()
    {
        var sparkle = Instantiate(sparklePrefab);

        sparkle.transform.SetParent(canvas.transform, false);

        var randomX = Random.Range(-960f, 960f);
        var randomY = Random.Range(-540f, 540f);
        sparkle.transform.localPosition = new Vector2(randomX, randomY);

        sparkle.transform.SetAsFirstSibling();
    }
}