using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Projectile : MonoBehaviour
{
    public static Projectile Create(Vector3 position, Enemy enemy)
    {
        Transform pfArrowProjectile = Resources.Load<Transform>("pfArrowProjectile");
        Transform projectileTransform = Instantiate(pfArrowProjectile, position, Quaternion.identity);

        Projectile projectile = projectileTransform.GetComponent<Projectile>();
        projectile.SetTarget(enemy);
        return projectile;
    }

    private Enemy targetEnemy;
    private Vector3 lastMoveDirection;
    private float timeToDie = 2f;

    private void Update()
    {
        Vector3 moveDirection;

        if (targetEnemy != null)
        {
            moveDirection = (targetEnemy.transform.position - transform.position).normalized;
            lastMoveDirection = moveDirection;
        }
        else
        {
            moveDirection = lastMoveDirection;
        }

        float moveSpeed = 20f;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVector(moveDirection));

        timeToDie -= Time.deltaTime;
        if (timeToDie <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private void SetTarget(Enemy targetEnemy)
    {
        this.targetEnemy = targetEnemy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            //Hit the enemy!
            int damageAmount = 10;
            enemy.GetComponent<HealthSystem>().Damage(damageAmount);

            Destroy(gameObject);
        }
    }
}
