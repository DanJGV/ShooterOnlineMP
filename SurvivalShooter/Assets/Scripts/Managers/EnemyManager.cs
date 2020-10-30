using UnityEngine;
using Mirror;
public class EnemyManager : NetworkBehaviour
{
   
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    GameObject[] players;
    PlayerHealth playerHealth;
    bool start = false;

    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

    private void Update()
    {
        if (start == false)
        {
            players = GameObject.FindGameObjectsWithTag("Player");

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<PlayerMovement>().isLocalPlayer)
                {
                    playerHealth = players[i].GetComponent<PlayerHealth>();
                    start = true;
                }
            }
        }
    }


    void Spawn ()
    {
        /*if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);*/
        RpcSpawn();
    }

    [ClientRpc]

    void RpcSpawn()
    {
        if (playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        GameObject e = Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        NetworkServer.Spawn(e);
    }
}
