using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

     public NavMeshAgent enemyAI;
     public Animator enemyAnimator;
     public Weapon enemyWeapon;
     public Statistics enemyStatistics;
     public Inventory enemyInventory;
     public FieldOfView characterFOV;

     public float targetFollowTimeout = 5f;

     private float cacheTime = 0f;

     [Header("Enemy State Color")]
     public SkinnedMeshRenderer skin;
     public Color patrolColor;
     public Color searchingColor;
     public Color agroColor;

     [Header("Patrolling")]
     public List<Transform> patrolWaypoints = new List<Transform>();

     private int currentWaypointCount;

     public Transform currentTarget;


     //Position
     public Quaternion newRotation;
     
     public enum MovementState
     {
          Idle,
          Walking,
          Running,
     }
     public enum BrainState
     {
          Patrolling,
          Searching,
          Attacking,
     }

     public MovementState movementState;
     public BrainState brainState;

     // Start is called before the first frame update
     void Start()
    {
          characterFOV = GetComponent<FieldOfView>();
          enemyWeapon = GetComponent<Weapon>();
          brainState = BrainState.Patrolling;
    }


    // Update is called once per frame
    void Update()
    {
          AnimationUpdater();
          UpdateEnemyStatusColor();

          if (brainState == BrainState.Patrolling)
          {
               EnemyPatrolling();
          } else if (brainState == BrainState.Searching)
          {

          } else if (brainState == BrainState.Attacking) {
               EnemyAttacking();
          }

          if (cacheTime > enemyStatistics.thinkingDelayTime)
          {
               BrainEngine();
               enemyStatistics.thinkingDelayTime = Random.Range(enemyStatistics.minThinkingTime, enemyStatistics.maxThinkingTime);
               cacheTime = 0;
          }

          

          cacheTime += Time.deltaTime;
    }

     private void OnDrawGizmos()
     {
          Gizmos.color = Color.green;

          Vector3 currentPosition = transform.position;

          for(int i = 0; i < patrolWaypoints.Count; i++)
          {
               Gizmos.DrawLine(currentPosition, patrolWaypoints[i].position);
               currentPosition = patrolWaypoints[i].position;
          }
     }

     public void BrainEngine()
     {
          
     }

     private void EnemyPatrolling()
     {
          if(patrolWaypoints.Count > 0)
          {
               if(enemyAI.remainingDistance < .5f)
               {
                    enemyAI.SetDestination(patrolWaypoints[currentWaypointCount].transform.position);

                    if(currentWaypointCount >= patrolWaypoints.Count - 1)
                    {
                         currentWaypointCount = 0;
                    } else
                    {     
                         currentWaypointCount += 1;
                    }

               }
          } else
          {
               if(enemyAI.remainingDistance < .5f)
               {
                    Vector3 randomDestination = new Vector3(Random.Range(transform.position.x - 5, transform.position.x + 5), 0, Random.Range(transform.position.z - 5, transform.position.z + 5));
                    enemyAI.SetDestination(randomDestination);
               }
          }
     }

     private void EnemyAttacking()
     {
          Statistics targetStats = currentTarget.GetComponent<Statistics>();

          if (!targetStats.isAlive) { brainState = BrainState.Patrolling; return; }

          if(Vector3.Distance(transform.position, currentTarget.position) > enemyWeapon.currentWeapon.fireRange)
          {
               enemyAI.isStopped = false;
               enemyAI.SetDestination(currentTarget.position);
          } else
          {
               
               enemyAI.isStopped = true;
               enemyWeapon.AttackTarget(currentTarget);
               enemyAnimator.SetTrigger("attack");
          }

          
     }

     private void UpdateEnemyStatusColor()
     {

          if(brainState == BrainState.Patrolling)
          {
               skin.material.color = patrolColor;
          } else if (brainState == BrainState.Searching)
          {
               skin.material.color = searchingColor;
          } else if (brainState == BrainState.Attacking)
          {
               skin.material.color = agroColor;
          }
     }

     private void ResetAnimationLayers()
     {
          for(int i = 0; i < enemyAnimator.layerCount; i++)
          {
               enemyAnimator.SetLayerWeight(i, 0f);
          }
     }

     private void SetAnimationLayer(int index, float weight)
     {
          ResetAnimationLayers();
          enemyAnimator.SetLayerWeight(index, weight);
     }

     //Animations state updater
     private void AnimationUpdater()
     {
          
          if(enemyAI.velocity.magnitude > .1)
          {
               
               enemyAnimator.SetInteger("movementState", 1);
          } else
          {
               enemyAnimator.SetInteger("movementState", 0);
          }

          if (enemyWeapon.currentWeapon.weaponCategory == SO_WeaponData.WeaponCategory.Unarmed)
          {
               SetAnimationLayer(0, 1f);
          }
          else if (enemyWeapon.currentWeapon.weaponCategory == SO_WeaponData.WeaponCategory.Knife)
          {
               SetAnimationLayer(0, 1f);
          }
          else if (enemyWeapon.currentWeapon.weaponCategory == SO_WeaponData.WeaponCategory.Pistol)
          {
               SetAnimationLayer(2, 1f);
          }
          else if (enemyWeapon.currentWeapon.weaponCategory == SO_WeaponData.WeaponCategory.Rifle)
          {
               SetAnimationLayer(1, 1f);
          }

     }

     #region Util methods
     
     

     #endregion


     #region ANIMATION EVENTS
     public void AttackAnimation(Weapon weaponID)
     {

     }

     public void FootStepAnimation()
     {

     }
     #endregion
}
