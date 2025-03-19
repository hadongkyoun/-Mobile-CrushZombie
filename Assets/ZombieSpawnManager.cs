using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    [SerializeField]
    private ObjectPoolingManager poolingManager;

    [SerializeField]
    private VanEngine vanEngine;

    [SerializeField]
    private int zombieHangNumMaximum;



    [SerializeField]
    private float zombieMinSpawnTime = 4.5f;
    [SerializeField]
    private float zombieMaxSpawnTime = 9.5f;

    private float currentTime;
    private float zombieSpawnTime;

    private void Start()
    {
        zombieSpawnTime = Random.Range(zombieMinSpawnTime, zombieMaxSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.playerDead)
            return;

        if(currentTime > zombieSpawnTime)
        {
            currentTime = 0.0f;
            zombieSpawnTime = Random.Range(zombieMinSpawnTime, zombieMaxSpawnTime);
            if(poolingManager != null)
            poolingManager.ActivateObstacle
                        (0, Vector3.zero, Quaternion.identity);
        }
        else
        {
            if (vanEngine.HangOnZombieNums < zombieHangNumMaximum)
            {
                currentTime += Time.deltaTime;
            }
        }
    }

}
