
using UnityEngine;

[CreateAssetMenu(fileName = "Obstacle Data", menuName = "Scriptable Object/Obstacle Data", order = int.MaxValue)]
public class Obstacle : ScriptableObject
{

    [SerializeField]
    private int id;
    public int Id { get { return id; } }
    [SerializeField]
    private float damageVelocity;
    public float DamageVelocity { get { return damageVelocity; } }
    [SerializeField]
    private float damageHp;
    public float DamageHp { get { return damageHp; } }

}
