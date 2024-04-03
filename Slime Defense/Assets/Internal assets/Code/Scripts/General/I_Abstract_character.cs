using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Abstract_character
{

    public void TakeDamage(float damage){}

    public void TakeDamage(float damage, string[] elements){}

    void Deth_Skill(){}
}
