using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Abstract_minion : Music, I_Abstract_character
{
    [SerializeField] public PoolControl.EnemyType type;
    [SerializeField] protected int count_gold;
    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 move_direction = Vector3.right;
    [SerializeField] protected float max_hp;    //понадобится на будущее, когда хилера добавим
    protected float hp;
    [Space]

    [Header ("Attack")]
    [SerializeField] protected float point_damage;
    [SerializeField] protected float delay_attack;
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
    protected  Animator animator;
    [SerializeField]  LayerMask layerMask;


    protected Collider target;
    
    private float taimer_for_attack;



    public static Action<int> onDied;


    protected void Awake(){
        hp = max_hp;
        animator = GetComponent<Animator>();
    }

    protected virtual void FixedUpdate()
    {
      
        
        var colliders = Physics.OverlapBox(transform.position, new Vector3(distance_attack, 4f, Utilities.Instance.GetHalfZ()), new Quaternion(0, 0, 0, 0), layerMask: layerMask); 
        if (colliders.Length > 0){


            float minX = colliders.Min(collider => collider.transform.position.x);
            target = colliders.First(x => x.transform.position.x == minX);

            animator.SetBool("isWalked", false);
            
            if (taimer_for_attack <= 0 && (target is not null)){
                
                animator.SetTrigger("attack");                                                 //в анимации заложен вызов функции AnimAttack()
                taimer_for_attack = delay_attack;
            } else {
                taimer_for_attack -= Time.fixedDeltaTime;
            }
        } else {
            transform.Translate(move_direction * speed * Time.fixedDeltaTime);
            animator.SetBool("isWalked", true);
        }
        

    }

    public void TakeDamage(float damage, Utilities.Elements[] elements = null)
    {
        hp -= damage;
        PlaySound(sounds[1]);
        Pool_text_damage.Instance.ShowDamage(damage, transform);
        if (hp <= 0) {
            PlaySound(sounds[0], volume: 1f , destroyed: true);
            onDied?.Invoke(count_gold);
            PoolControl.Instance.DestroyObject(gameObject);
        }
    }


    protected virtual void AutoAttack(Collider target)
    {
        target.gameObject.GetComponent<I_Abstract_character>().TakeDamage(point_damage);
    }

    protected void AnimAttack(){
        this.AutoAttack(target);
    }

}
