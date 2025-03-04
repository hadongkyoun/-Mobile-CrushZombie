
using System.Collections;
using UnityEngine;




public class VanController : Subject
{
    private InputHandler inputHandler;
    private CameraHandler cameraHandler;
    private VanUIUpdater vanUIUpdater;

    private VanDrive vanDrive;
    private VanEngine vanEngine;
    private CrushManager crushManager;

    // Physics
    private Rigidbody rigidbody;

    private VanData data;

    [SerializeField]
    private GameObject vanExplode;

    private float dir;
    private float firstPosZ;
    

    #region Property
    private float currentHP;
    public float CurrentHP { get { return currentHP; } }
    private int combo;
    public float Combo { get { return combo; } }
    private float boosterAmount;
    public float BoosterAmount { get { return boosterAmount; } }

    #endregion




    private void Awake()
    {

        cameraHandler = Camera.main.transform.GetComponent<CameraHandler>();
        Application.targetFrameRate = 60;
        rigidbody = GetComponent<Rigidbody>();

        vanUIUpdater = UIManager.Instance.transform.GetComponentInChildren<VanUIUpdater>();

        

    }

    private void Start()
    {
        data = new VanData();


        // 옵저버 세팅
        if (vanUIUpdater != null)
            Attach(vanUIUpdater);
        if (cameraHandler != null)
        {
            Attach(cameraHandler);
        }

        inputHandler = new InputHandler();
        vanEngine = new VanEngine(data, vanUIUpdater);
        vanDrive = new VanDrive(data, rigidbody, transform);
        crushManager = new CrushManager(data, vanUIUpdater);

        firstPosZ = transform.position.z;
    }


    private void Update()
    {

        // 조작 기능
        dir = inputHandler.SetDirection();

        // 엔진 기능
        vanEngine.RunningEngine();

        // 엔진의 내구도가 0이거나 속도가 0이 될때 게임 종료
        if (crushManager.GetVanHP() <= 0 || vanEngine.GetVanSpeed() <= 0)
        {
            vanUIUpdater.SendResultInfo(firstPosZ, transform.position.z, crushManager.ZombieKillNum());
            DestoryVan();
            NotifyObservers();
            return;
        }
    }



    private void FixedUpdate()
    {
        // 드라이브 기능
        vanDrive.Drive(vanEngine.GetVanSpeed(), dir);
    }


    // 다른 오브젝트와 On Trigger로 부터 발동
    public void ObjectCollision(Obstacle obstacleData)
    {
        // 차의 속도와 부스터에 영향 
        vanEngine.AffectEngine(obstacleData);
        // 차의 내구도와 콤보에 영향
        crushManager.AffectVan(obstacleData);
        combo = crushManager.UpdateCombo(obstacleData);

        // 콤보가 5배수면 속도 증가
        if (crushManager.IsMultiplesOfFive())
        {
            vanEngine.ComboSpeedUP();
        }

        // 현재 정보 업데이트
        currentHP = crushManager.GetVanHP() / data.maxVanHP;
        boosterAmount = vanEngine.GetBoosterAmount();

        // UI 업데이트
        NotifyObservers();
    }


    private void DestoryVan()
    {
        currentHP = 0;
        Instantiate(vanExplode, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void FinishAvoiding()
    {
        vanDrive.LimitPlayerDriving();
        StartCoroutine(BoosterReady());
    }

    IEnumerator BoosterReady()
    {
        while (!(transform.position.x > -0.1f && transform.position.x < 0.1f))
        {
            yield return null;
        }
        vanEngine.LastSpurt();
        crushManager.LastSpurt();
    }


}
