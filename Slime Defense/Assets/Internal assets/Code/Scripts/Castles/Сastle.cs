using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Сastle : Abstract_friend
{
    protected override void Deth_Skill()
    {
        Menu_scripts.Instance.GameOver();
    }
}
