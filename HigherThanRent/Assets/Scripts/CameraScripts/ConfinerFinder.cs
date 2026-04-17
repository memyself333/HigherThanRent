using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.SceneManagement;

public class ConfinerFinder : MonoBehaviour
{
    // When enabled, subscribe the function OnSceneLoaded to the sceneLoaded event, so that when a new scene is loaded, the function will be called and the confiner will be assigned to the camera
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // When disabled, unsubscribe the function OnSceneLoaded from the sceneLoaded event
    private void OnDisable()
    { 
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Function to find the confiner in the scene and assign it to the CinemachineConfiner2D component on the camera
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CinemachineConfiner2D confiner = GetComponent<CinemachineConfiner2D>();
        confiner.BoundingShape2D = GameObject.FindGameObjectWithTag("Confiner").GetComponent<BoxCollider2D>();
    }
}