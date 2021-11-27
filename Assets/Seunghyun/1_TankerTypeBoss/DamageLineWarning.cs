using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLineWarning : MonoBehaviour
{
    [SerializeField] private GameObject damageLine;

    private void Start()
    {
        Instantiate(damageLine, transform.position, Quaternion.identity);
    }

}
