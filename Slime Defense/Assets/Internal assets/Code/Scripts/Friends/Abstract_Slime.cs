using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Abstract_Slime : Music, I_Abstract_character
{
    [Header ("Specifications")]
    [SerializeField] public PoolControl.SlimeType type;
    [Space]
    [SerializeField] protected float hp;
    [SerializeField] protected float speed;
    [SerializeField] protected Vector3 move_direction = Vector3.left;
    [Space]
    [Header ("Attack")]
    [SerializeField] protected float distance_attack = 20f;
    [SerializeField] protected float delay_attack;
    [SerializeField] protected float point_damage;

    [Header ("Components")]
    protected Animator animator;
    [SerializeField] protected LayerMask layerMask;
    // Start is called before the first frame update


    protected Utilities.Elements[] elements;
    protected Collider target;
    protected float taimer_for_attack;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {

        
        var colliders = Physics.OverlapBox(transform.position, new Vector3(distance_attack, 4f, Utilities.Instance.GetHalfZ()), new Quaternion(0, 0, 0, 0), layerMask: layerMask); 
        if (colliders.Length > 0){

            float minX = colliders.Min(collider => collider.transform.position.x);
            target = colliders.First(x => x.transform.position.x == minX);


            if (taimer_for_attack <= 0 && (target is not null)){
                animator.SetBool("isWalked", false);
                animator.SetTrigger("attack");  // //в анимации заложен вызов функции AnimAttack()
                taimer_for_attack = delay_attack;
            } else {
                taimer_for_attack -= Time.fixedDeltaTime;
            }
        } else {
            transform.Translate(move_direction * speed * Time.fixedDeltaTime);
            animator.SetBool("isWalked", true);
        }
        

    }

    protected virtual void AnimAttack(){
        this.AutoAttack(target);
    }

    protected void AutoAttack(Collider target)
    {
        target.gameObject.GetComponent<I_Abstract_character>().TakeDamage(point_damage, elements);
    }

    public  void TakeDamage(float damage,  Utilities.Elements[] elements = null){
        this.hp -= damage;
        PlaySound(sounds[1], destroyed: true);
        Pool_text_damage.Instance.ShowDamage(damage, transform);
        if (this.hp <= 0) Deth_Skill();
    }

    protected virtual void Deth_Skill()
    {
        //описание посмертного умения слайма
        PoolControl.Instance.DestroyObject(gameObject);
    }


    private void MoveSound(){

    }
}
