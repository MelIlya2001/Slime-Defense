using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Abstract_projectile : MonoBehaviour
{
    [SerializeField] public PoolControl.ProjectileType type;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Utilities.Elements[] elements;


    
    void FixedUpdate(){
        transform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other){
        if ((layerMask.value & (1 << other.gameObject.layer)) > 0) 
            other.GetComponent<I_Abstract_character>().TakeDamage(damage, elements);
        
        PoolControl.Instance.DestroyObject(gameObject);
    }

}
