using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, I_Abstract_character
{
    [SerializeField] private float hp;
    [SerializeField] private float lifetime;
    private Animator animator;
    private float current_hp;
    private Transform parent;
    private float timer;

    public void Awake(){
        animator = GetComponent<Animator>();
        parent = transform.parent;
    }

    void OnEnable(){
        //animator.SetTrigger("Spawn");
        current_hp = hp;
        timer = lifetime;
        transform.parent = null;
        transform.position = new Vector3(parent.position.x, 3f, parent.position.z);
    }


    void FixedUpdate(){
        timer -= Time.fixedDeltaTime;
        if (timer <= 0) animator.SetTrigger("Destroy");
    }


    public void TakeDamage(float damage, Utilities.Elements[] elements = null){
        current_hp -= damage;
        if (current_hp <= 0) animator.SetTrigger("Destroy");
    }

    void Deth_Skill(){
        transform.parent = parent;
        gameObject.SetActive(false);
    }
}
