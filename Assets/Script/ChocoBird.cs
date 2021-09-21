using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChocoBird : Bird
{

    [SerializeField]
    public float boomArea = 100.0f;
    public float BoomRadius = 4.0f;
    public GameObject _explosionEffect;
    private bool _hasExploded = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        SetState(BirdState.HitSomething);

        if (!_hasExploded && (col.gameObject.CompareTag("Enemy") || col.gameObject.CompareTag("Obstacle")))
        {
            Explode();
        }
    }

    private void Explode()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y, -2.5f);
        GameObject explosion = Instantiate(_explosionEffect, pos, Quaternion.identity);
        Destroy(explosion, 2f);

        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, BoomRadius);
        foreach (Collider2D obj in objects)
        {
            if ((obj.gameObject.CompareTag("Enemy") || obj.gameObject.CompareTag("Obstacle")))
            {
                Vector2 direction = obj.transform.position - transform.position;
                obj.GetComponent<Rigidbody2D>().AddForce(direction * boomArea);
            }
        }

        _hasExploded = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, BoomRadius);
    }
}
