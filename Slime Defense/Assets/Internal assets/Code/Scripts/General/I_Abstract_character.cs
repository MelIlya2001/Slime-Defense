using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_Abstract_character
{

    //public void TakeDamage(float damage){}

    public void TakeDamage(float damage, Utilities.Elements[] elements = null){}

    void Deth_Skill(){}
}
