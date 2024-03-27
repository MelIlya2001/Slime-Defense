using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Abstract_Slime : Abstract_friend
{
    enum Element{
        Fire,
        Water,
        Air,
        Terra
    }



    [SerializeField] protected string[] elements;
    [Space]

    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 move_direction = Vector3.left;
    [Space]

    [SerializeField] protected float distance_attack = 20f;
    [SerializeField] protected float delay_attack;
    [SerializeField] protected float point_damage;

    [Header ("Components")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected LayerMask layerMask;
    // Start is called before the first frame update


    protected RaycastHit hit;
    protected float taimer_for_attack;
    
    void Start()
    {
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        
        if (Physics.Raycast(transform.position, move_direction, out hit, distance_attack, layerMask)){
            if (taimer_for_attack <= 0){
                AutoAttack(hit);
                taimer_for_attack = delay_attack;
            } else {
                taimer_for_attack -= Time.fixedDeltaTime;
            }
        } else {
            rb.AddForce(move_direction * speed, ForceMode.VelocityChange);
        }

    }


    protected void AutoAttack(RaycastHit hit)
    {
        hit.collider.gameObject.GetComponent<Abstract_enemy>().TakeDamage(point_damage, elements);
    }
}
