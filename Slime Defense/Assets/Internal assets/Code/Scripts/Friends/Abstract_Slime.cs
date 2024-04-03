using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Abstract_Slime : MonoBehaviour, I_Abstract_character
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
    [SerializeField] protected Animator animator;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected LayerMask layerMask;
    // Start is called before the first frame update


    protected string[] elements;
    protected RaycastHit hit;
    protected float taimer_for_attack;
    
    void Awake()
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
        hit.collider.gameObject.GetComponent<I_Abstract_character>().TakeDamage(point_damage, elements);
    }

    public  void TakeDamage(float damage){
        this.hp -= damage;
        Pool_text_damage.Instance.ShowDamage(damage, transform);
        if (this.hp <= 0) Deth_Skill();
    }

    private void Deth_Skill()
    {
        //описание посмертного умения слайма
        PoolControl.Instance.DestroyObject(gameObject);
    }
}
