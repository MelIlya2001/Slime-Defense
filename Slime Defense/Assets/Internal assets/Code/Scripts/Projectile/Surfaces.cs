using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surfaces : MonoBehaviour
{

    private Animator animator;
    [SerializeField] private Vector3 size_surface;
    [SerializeField] private float point_damage;
    [SerializeField] private Utilities.Elements[] elements;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float lifetime;
    [SerializeField] private float delay_damage;
    private float timer;
    private Transform parent;
    private float life_timer;
 
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        timer = delay_damage;
        parent = transform.parent;
    }

    void OnEnable(){
        //animator.SetTrigger("Spawn");
        life_timer = lifetime;
        transform.parent = null;
        transform.position = new Vector3(parent.position.x, 0.1f, parent.position.z);
    }


    void FixedUpdate(){
        timer -= Time.fixedDeltaTime;
        if (timer <= 0) {
            Damage();
            timer = delay_damage;
        }
        
        life_timer -= Time.fixedDeltaTime;
        if (life_timer <= 0) animator.SetTrigger("Destroy"); 
        
    }

    public void Damage(){
        Collider[] colliders = Physics.OverlapBox(transform.position, size_surface, new Quaternion(0,0,0,0), layerMask);
        if (colliders.Length <= 0)  return;
        foreach (Collider collider in colliders)
        {
            collider.gameObject.GetComponent<I_Abstract_character>().TakeDamage(point_damage, elements);
        }
    }

    private void Despwan(){
        transform.parent = parent;
        gameObject.SetActive(false);
    }
}
