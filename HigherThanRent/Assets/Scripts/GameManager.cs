using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Array to specify which objects should persist across scenes, must assign in inspector
    [Header("Persistent Objects")]
    public GameObject[] persistentObjects;

    // Function to check that only one instance of GameManager exists. If not, then destroy the new instance and all other persistent objects
    private void Awake()
    {
        if (instance != null)
        {
            CleanUpAndDestroy();
            return;
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            MarkPersistentObjects();
        }
    }
    // Function to mark all specified objects as persistent across scenes
    private void MarkPersistentObjects()
    {
        foreach (GameObject obj in persistentObjects)
        {
            if (obj != null)
            {
                DontDestroyOnLoad(obj);
            }
        }

    }

    // Function to destroy all persistent objects, then the GameManger instance. 
    private void CleanUpAndDestroy()
        {
        foreach (GameObject obj in persistentObjects)
        {
           Destroy(obj);
        }
        Destroy(gameObject);
    }
}
