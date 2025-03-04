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
                // 게임 전역에 싱글톤을 가지고있는 <T> 클래스가 있는지 확인
                instance = (T)FindFirstObjectByType(typeof(T));

                if(instance == null)
                {
                    // 없으면 싱글톤 컴포넌트 부착해서 연결
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
