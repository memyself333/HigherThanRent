using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    public Vector2 playerPosition;
    public string currentScene;
    public bool axeAcquired;
    public bool enemyDead;
    public Vector2 enemyPosition;
    public bool commonRoomDoorOpen;
    public bool lobbyDoorOpen;
    public bool chestOpen;
    public bool hallucinated;

}
