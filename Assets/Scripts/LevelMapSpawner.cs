using UnityEngine;

public class LevelMapSpawner : MonoBehaviour
{
    public GameObject levelPrefab;
    public int levelCount = 10;
    public float spacing = 300f;

    void Start()
    {
        SpawnNodes();
    }

    void SpawnNodes()
    {
        // CLEAR OLD NODES
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Center alignment
        float startOffset =
            -((levelCount - 1) * spacing) / 2f;

        for (int i = 0; i < levelCount; i++)
        {
            GameObject level =
                Instantiate(levelPrefab, transform);

            RectTransform rt =
                level.GetComponent<RectTransform>();

            // Stable UI settings
            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.pivot = new Vector2(0.5f, 0.5f);
            rt.localScale = Vector3.one;

            float x = startOffset + i * spacing;

            rt.anchoredPosition =
                new Vector2(x, 0f);

            LevelNode node =
                level.GetComponent<LevelNode>();

            node.Setup(i + 1);
        }

        Debug.Log(
            "Unlocked Level = " +
            PlayerPrefs.GetInt("UnlockedLevel", 1)
        );
    }
}