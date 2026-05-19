using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelSelect : MonoBehaviour
{
    [Header("References")]
    public Transform nodeParent;   // Assign: Scroll View → Content

    private List<Transform> nodes = new List<Transform>();

    void Start()
    {
        GetAllNodes();

    }

    // 🔹 Collect all level nodes
    void GetAllNodes()
    {
        nodes.Clear();

        foreach (Transform group in nodeParent) // e.g., LevelNodes container
        {
            foreach (Transform node in group)   // actual nodes
            {
                nodes.Add(node);
            }
        }

        Debug.Log("Nodes Found: " + nodes.Count);
    }

    // ---------------- LEVEL LOADING ----------------

    public void LoadLevel(int index)
    {
        if (ScreenTransition.instance != null)
        {
            ScreenTransition.instance.FadeAndLoad(index, 1f);
        }
        else
        {
            SceneManager.LoadScene(index);
        }

        Debug.Log(
         "Saved Unlock = " +
       PlayerPrefs.GetInt("UnlockedLevel", 1)
        );
    }

    public void BackToMenu()
    {
        if (ScreenTransition.instance != null)
        {
            ScreenTransition.instance.FadeAndLoad(0, 1f);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}