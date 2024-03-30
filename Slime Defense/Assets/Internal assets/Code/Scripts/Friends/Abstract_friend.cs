using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abstract_friend : MonoBehaviour
{

    [Header ("Specifications")]
    [SerializeField] protected float hp;
    // Start is called before the first frame update

    public void TakeDamage(float damage)
    {
        this.hp -= damage;
        
        if (this.hp <= 0) Deth_Skill();
    }

    protected virtual void Deth_Skill()
    {
        //описание посмертного умения слайма
        Destroy(this.gameObject);
        Debug.Log("НеСюда");
    }
}
