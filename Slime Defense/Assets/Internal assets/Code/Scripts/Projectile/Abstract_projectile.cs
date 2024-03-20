using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_projectile : MonoBehaviour
{
    [SerializeField] public Castle_enemy.ProjectileInfo.ProjectileType type;
}
