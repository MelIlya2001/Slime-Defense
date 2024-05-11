using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T1_water : Abstract_Slime
{
    
    protected override void AutoAttack(Collider target)
    {

        GameObject bullet = PoolControl.Instance.GetObject(PoolControl.ProjectileType.element_water, PoolControl.Instance.projectile_pools);    
        bullet.transform.position = transform.position + new Vector3(5,3,0);
        var bound = target.GetComponent<BoxCollider>().bounds;
        bullet.transform.LookAt(new Vector3(target.transform.position.x, bound.max.y, target.transform.position.z));
    }
}
