using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class VanEngine : MonoBehaviour
{

    [SerializeField]
    private VanData vanData;

    [SerializeField]
    private GameObject vanExplode;

    private float vanBeforeSpeed;
    private float vanStraightSpeed;
    public float VanStraightSpeed { get { return vanStraightSpeed; } }
    private float vanMinSpeed;

    private float increaseValue;

    private float engineHP;
    public float MaxHP { get { return engineHP; } }
    private float currentHP;
    public float CurrentHP { get { return currentHP; } }

    private bool updateSpeedTrigger;
    public bool UpdateSpeedTrigger { get { return updateSpeedTrigger; } set { updateSpeedTrigger = value; } }


    private float firstVanPosZ;

    private int hangOnZombieNums = 0;
    public int HangOnZombieNums { get { return hangOnZombieNums; } set { hangOnZombieNums = value; } }
    private void Start()
    {
        // ���� ���
        vanStraightSpeed = vanData.StraightSpeed;
        vanMinSpeed = vanData.StraightMinimumSpeed;
        increaseValue = vanData.IncreaseValue;

        // ���� ������
        engineHP = vanData.MaxVanHP;
        currentHP = engineHP;

        firstVanPosZ = transform.position.z;
    }

    private void Update()
    {
        RunningEngine();

        if(hangOnZombieNums > 0)
        {
            currentHP -= Time.deltaTime / 3.0f;
        }

        if(currentHP < 0)
        {
            DestoryVan();
        }
    }

    public void RunningEngine()
    {

        vanStraightSpeed += Time.deltaTime * increaseValue;

        // ���� �ӵ��� ������ �������� ���
        if (vanStraightSpeed <= vanMinSpeed)
        {
            vanStraightSpeed = 0f;
        }
        // UI ������Ʈ
        if (Mathf.Abs(vanBeforeSpeed - vanStraightSpeed) >= 1.0f)
        {
            vanBeforeSpeed = vanStraightSpeed;
            updateSpeedTrigger = true;
        }
    }

    public void AffectEngineVelocity(float _damage)
    {
        float damage = _damage;
        if (damage <= 0.1f)
        {
            damage = 0.1f;
        }

        vanStraightSpeed -= damage;
    }

    public void AffectEngineHP(float _damage)
    {
        currentHP -= _damage;
    }

    private void DestoryVan()
    {
        currentHP = 0;
        Instantiate(vanExplode, transform.position, transform.rotation);
        GameManager.Instance.playerMoveDistance = (transform.position.z - firstVanPosZ) / 10;
        GameManager.Instance.playerDead = true;
        Destroy(gameObject);
    }




}
