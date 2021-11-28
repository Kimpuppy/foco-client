using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageLineWarning : MonoBehaviour
{
    [SerializeField] private GameObject damageLine;

    private void Start()
    {
        Invoke(nameof(OnDamageLine), 0.5f);
    }

    private void OnDamageLine()
    {
        Instantiate(damageLine, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
