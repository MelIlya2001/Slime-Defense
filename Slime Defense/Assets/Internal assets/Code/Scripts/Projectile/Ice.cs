using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private Vector3 move_direction;
    [SerializeField] private Vector3 move_rotation;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Utilities.Elements[] elements;
    private Transform parent;
    private List<Collider> entered_colliders;


    void Awake(){
        parent = transform.parent;
        entered_colliders = new List<Collider>();
    }

    void OnEnable(){
        transform.parent = null;
        transform.position = parent.position;
        entered_colliders.Clear();
    }

    void FixedUpdate(){
        transform.Translate(move_direction * speed * Time.fixedDeltaTime);
        transform.Rotate(move_rotation * Time.fixedDeltaTime);

        
        Damage();
        

        if (transform.position.x > Utilities.Instance.GetEndLevel().x + 30) {
            transform.gameObject.SetActive(false);
            transform.parent = parent;
            }

    }

    public void Damage(){
        Collider[] colliders = Physics.OverlapBox(transform.position, new Vector3(1, 1, 20), new Quaternion(0,0,0,0), layerMask);
        if (colliders.Length <= 0)  return;
        foreach (Collider collider in colliders)
        {
            if (entered_colliders.Contains(collider)) continue;
            collider.gameObject.GetComponent<I_Abstract_character>().TakeDamage(damage, elements);
            entered_colliders.Add(collider);
        }
    }

}
