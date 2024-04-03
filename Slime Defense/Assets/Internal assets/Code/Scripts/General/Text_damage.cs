using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text_damage : MonoBehaviour
{
    private void Deactivate(){
        Pool_text_damage.Instance.Remove(this.gameObject);
    }
}
