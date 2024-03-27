using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abstract_enemy : MonoBehaviour
{

    [Header ("Specifications")]
    [SerializeField] protected float hp;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(float damage, string[] elements){
        hp -= damage;
        if (hp <= 0){
            Debug.Log("Смерть моя");
            Destroy(this.gameObject);
        }
    }
}
