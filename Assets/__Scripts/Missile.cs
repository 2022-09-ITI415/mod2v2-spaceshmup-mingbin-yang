using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public int index = 0;

    [Header("REFERENCES")]
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private GameObject _target;
    [SerializeField] private GameObject _explosionPrefab;

    [Header("MOVEMENT")]
    public  float speed = 15;
    [SerializeField] private float _rotateSpeed = 95;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        FindTarget();
        Debug.Log("Target Set: " + _target);
        if (!_target)
        {
            //Destroy(gameObject);
        }
    }

    void FindTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float distance = float.MaxValue;
        if (enemies.Length == 0)
        {
            return;
        }

        foreach (Enemy enem in enemies)
        {
            float dist = Vector3.Distance(transform.position, enem.transform.position);
            if (dist < distance)
            {
                 _target = enem.gameObject;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_target)
        {
            FindTarget();
            MoveForward();
            return;
        }

        Vector3 dir = _target.transform.position - transform.position;
        transform.LookAt(_target.transform.position);
        MoveForward();
    }

    void MoveForward()
    {
        _rb.velocity = transform.forward * speed;
    }

    public void ExplodeVFX()
    {
        Debug.Log("Explode!");
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        FindObjectOfType<Hero>().GetComponent<AudioSource>().PlayOneShot(Main.S.explosionSFX);
    }
}
