using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Air_slime_rdd : Abstract_Slime
{
   

    /*protected override void FixedUpdate()
    {
        Vector3 fly_detecting = transform.position + new Vector3(0, 8.5f, 0);

        if (Physics.Raycast(transform.position, move_direction, out hit, distance_attack, layerMask) || Physics.Raycast(fly_detecting, move_direction, out hit, distance_attack, layerMask)) {
            if (taimer_for_attack <= 0){
                AutoAttack(hit);
                taimer_for_attack = delay_attack;
            } else {
                taimer_for_attack -= Time.fixedDeltaTime;
            }
        } else {
            rb.AddForce(move_direction * speed, ForceMode.VelocityChange);
        }

    }*/
}
