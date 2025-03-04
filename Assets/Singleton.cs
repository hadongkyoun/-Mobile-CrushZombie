using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                // ���� ������ �̱����� �������ִ� <T> Ŭ������ �ִ��� Ȯ��
                instance = (T)FindFirstObjectByType(typeof(T));

                if(instance == null)
                {
                    // ������ �̱��� ������Ʈ �����ؼ� ����
                    GameObject obj = new GameObject(typeof(T).Name, typeof(T));
                    instance = obj.GetComponent<T>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
