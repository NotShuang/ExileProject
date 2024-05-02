using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public Transform respawnPoint; // A reference to the respawn point in the scene
    public GameObject playerPrefab; // A reference to the player prefab

    private GameObject player; // A reference to the currently active player instance

    private void Start()
    {
        // Instantiate the player at the respawn point
        player = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }

    public void RespawnPlayer()
    {
        // Destroy the current player instance
        Destroy(player);

        // Instantiate a new player instance at the respawn point
        player = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }
}