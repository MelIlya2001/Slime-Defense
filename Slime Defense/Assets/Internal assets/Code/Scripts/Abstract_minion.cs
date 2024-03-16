using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abstract_minion : Abstract_enemy
{
    [SerializeField] protected float count_gold;
    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 move_direction = Vector3.right;
    [Space]

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public override void TakeDamage(float damage, string[] elements)
    {
        this.hp -= damage;
        Debug.Log("Больно в ноге");
        if (this.hp <= 0) Deth_Skill();
    }

    protected void Deth_Skill()
    {
        //описание посмертного умения слайма
        Destroy(this.gameObject);
    }

    protected void AutoAttack(RaycastHit hit)
    {
        hit.collider.gameObject.GetComponent<Abstract_friend>().TakeDamage(point_damage);
    }
}
