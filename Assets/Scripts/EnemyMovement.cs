using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float patrolRadius = 10f;
    public float speedWalk = 2f;
    public float speedRun = 5f;
    public Transform patrolCenter;
    public Transform player;
    public float detectionRange = 15f;
    public float attackRange = 5f;
    public GameObject enemy;

    private Animator animator;
    private Vector3 patrolPoint;

    private enum EnemyState { Patrol, Chase, Attack }
    private EnemyState currentState = EnemyState.Patrol;

    void Start()
    {
        animator = enemy.GetComponent<Animator>();
        PickRandomPatrolPoint();
    }

    void Update()
    {
        if (enemy != null)
        {
            Enemy enemyPlayer = enemy.GetComponent<Enemy>();

            if (enemyPlayer.health <= 0)
            {
                animator.Play("Walking To Dying"); // Mixamo death animation name
                Destroy(enemy, 3f); // Destroy enemy after 3 seconds
                return; // Exit if enemy is dead
            }


            float distanceToPlayer = Vector3.Distance(enemy.transform.position, player.position);

            // Switch states based on player distance
            if (distanceToPlayer <= attackRange)
                currentState = EnemyState.Attack;
            else if (distanceToPlayer <= detectionRange)
                currentState = EnemyState.Chase;
            else
                currentState = EnemyState.Patrol;

            // Perform actions based on state
            switch (currentState)
            {
                case EnemyState.Patrol:
                    Patrol();
                    break;
                case EnemyState.Chase:
                    ChasePlayer();
                    break;
                case EnemyState.Attack:
                    AttackPlayer();
                    break;
            }
        }
        else
        {
            return; // Exit if enemy is null
        }
    }
        

    void Patrol()
    {
        animator.Play("Rifle Walk"); // Mixamo walk animation name
        MoveTowards(patrolPoint, speedWalk);

        if (Vector3.Distance(enemy.transform.position, patrolPoint) < 0.5f)
            PickRandomPatrolPoint();
    }

    void ChasePlayer()
    {
        animator.Play("Run Forward"); // Mixamo run animation name
        MoveTowards(player.position, speedRun);
    }

    void AttackPlayer()
    {
        animator.Play("Shoot Rifle"); // Mixamo shooting animation name
        FaceTarget(player.position);
    }

    void MoveTowards(Vector3 targetPos, float moveSpeed)
    {
        Vector3 direction = (targetPos - enemy.transform.position).normalized;
        enemy.transform.position += direction * moveSpeed * Time.deltaTime;
        FaceTarget(targetPos);
    }

    void FaceTarget(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - enemy.transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }


    void PickRandomPatrolPoint()
    {
        Vector2 randomCircle = Random.insideUnitCircle * patrolRadius;
        patrolPoint = patrolCenter.position + new Vector3(randomCircle.x, 0, randomCircle.y);
    }
}
