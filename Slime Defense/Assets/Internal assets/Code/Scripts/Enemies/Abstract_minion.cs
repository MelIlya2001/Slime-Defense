using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abstract_minion : MonoBehaviour, I_Abstract_character
{
    [SerializeField] public PoolControl.EnemyType type;
    [SerializeField] protected float count_gold;
    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 move_direction = Vector3.right;
    [SerializeField] protected float max_hp;    //понадобится на будущее, когда хилера добавим
    protected float hp;
    [Space]

    [Header ("Attack")]
    [SerializeField] protected float point_damage;
    [SerializeField] protected float speed_attack;
    [SerializeField] protected float distance_attack;
    [SerializeField] protected float splash_damage;
    [Space]

    [Header ("Resistance")]
    [SerializeField] protected float r_fire;
    [SerializeField] protected float r_water;
    [SerializeField] protected float r_terra;

    [SerializeField] protected float r_air;
    [SerializeField] protected float r_status;

    [Header ("Components")]
    [SerializeField]  Animator animator;
    [SerializeField]  Rigidbody rb;
    [SerializeField]  LayerMask layerMask;


    protected RaycastHit hit;
    private float taimer_for_attack;

    protected void Awake(){
        hp = max_hp;
    }

    protected virtual void FixedUpdate()
    {
        
        if (Physics.Raycast(transform.position, move_direction, out hit, distance_attack, layerMask)){
            if (taimer_for_attack <= 0){
                AutoAttack(hit);
                taimer_for_attack = speed_attack;
            } else {
                taimer_for_attack -= Time.fixedDeltaTime;
            }
        } else {
            rb.AddForce(move_direction * speed, ForceMode.VelocityChange);
        }

    }

    public  void TakeDamage(float damage, string[] elements)
    {
        hp -= damage;
        Pool_text_damage.Instance.ShowDamage(damage, transform);
        if (hp <= 0) PoolControl.Instance.DestroyObject(gameObject);
    }


    protected void AutoAttack(RaycastHit hit)
    {
        hit.collider.gameObject.GetComponent<I_Abstract_character>().TakeDamage(point_damage);
    }
}
