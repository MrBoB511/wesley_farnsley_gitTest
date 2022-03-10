using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    private Collider projColl;
    // Start is called before the first frame update
    void Start()
    {
        projColl = GetComponent<CapsuleCollider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
        }
        transform.Translate((target.transform.position - transform.position).normalized * 10 * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
