using UnityEngine;

public class ClickUpgradeHandler : MonoBehaviour
{
    [SerializeField]
    private int id;

    private UpgradeManager upgradeManager;
    private Animation animation;

    private void Awake()
    {
        animation = GetComponent<Animation>();
        upgradeManager = GetComponentInParent<UpgradeManager>();
    }


    public void Upgrade()
    {
        switch (id)
        {
            case 0:
                upgradeManager.UpgradeSpeedData();
                break;
            case 1:
                upgradeManager.UpgradeDurabilityData();
                break;
            case 2:
                upgradeManager.UpgradeOfflineMoneyData();
                break;
        }

        if (animation.isPlaying)
        {
            animation.Stop();
        }

        animation.Play();
        AudioManager.Instance.PlaySFX(AudioManager.SFX.SFX_UPGRADE);
    
    
    }
}
