using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Сastle : MonoBehaviour
{

    [SerializeField] private float HP;
    // Start is called before the first frame update
    void Start()
    {
       this.HP = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Take_Damage(float damage)
    {
        this.HP -= damage;
        if (this.HP <= 0) {
            if (this.name == "Castle_Hero") {   
                YouLose();
                return;
            } else {
                YouWin();
                return;
            }
        }
    }

    void YouWin(){}

    void YouLose(){}
}
