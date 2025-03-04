using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPoolingManager : MonoBehaviour
{

    [SerializeField]
    private GameObject[] prefabs;



    private Dictionary<int, Queue<GameObject>> QueueSet = new Dictionary<int, Queue<GameObject>>();
    #region QueueList
    private Queue<GameObject> jombies = new();
    private Queue<GameObject> trees = new();
    private Queue<GameObject> rocks = new();
    private Queue<GameObject> barrels = new();

    private Queue<GameObject> jombieEffect = new();
    private Queue<GameObject> treeEffect = new();
    private Queue<GameObject> rockEffect = new();
    private Queue<GameObject> barrelEffect = new();
    #endregion
    private void Awake()
    {

        #region QueueSet Setting
        // Objects
        QueueSet.Add(0, jombies);
        QueueSet.Add(1, trees);
        QueueSet.Add(2, rocks);
        QueueSet.Add(3, barrels);

        // Effect
        QueueSet.Add(4, jombieEffect);
        QueueSet.Add(5, treeEffect);
        QueueSet.Add(6, rockEffect);
        QueueSet.Add(7, barrelEffect);
        #endregion
    }
    public GameObject ActivateObstacle(int id, Vector3 pos, Quaternion rot)
    {
        // �ش� ��ֹ��� ���� Queue ã��
        if (QueueSet.TryGetValue(id, out Queue<GameObject> queue))
        {
            // �����ƴ� ������Ʈ�� ���� ���
            if (queue.Count == 0)
            {
                // ������Ʈ ����
                GameObject obj = Instantiate(prefabs[id], pos, rot);

                if (obj.TryGetComponent<ObstacleManager>(out ObstacleManager obstacleManager))
                {
                    obstacleManager.Initialization(this);
                }
                return obj;
            }
            // ������ ������Ʈ�� Queue�� �ִ� ���
            else
            {
                // ��Ȱ��ȭ
                GameObject obj = queue.Dequeue();
                // ��ġ ����
                obj.transform.position = pos;
                obj.transform.rotation = rot;

                obj.SetActive(true);
                return obj;
            }
        }
        else
        {
            return null;
        }
    }
    public void DeActivateObject(int id, GameObject obj)
    {
        if (QueueSet.TryGetValue(id, out Queue<GameObject> queue))
        {
            queue.Enqueue(obj);
            obj.SetActive(false);
        }

    }

    public void ActivateEffect(int id, Transform origin)
    {
        StartCoroutine(EffectPooling(id, origin));
    }

    IEnumerator EffectPooling(int id, Transform origin)
    {
        GameObject obj = ActivateObstacle(id, origin.position, origin.rotation);
        yield return new WaitForSecondsRealtime(1.7f);
        DeActivateObject(id, obj);
    }


}
