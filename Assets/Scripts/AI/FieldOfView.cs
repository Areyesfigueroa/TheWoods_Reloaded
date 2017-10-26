using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TheWoods.Player;
using TheWoods.Audio;

namespace TheWoods.AI
{
    public class FieldOfView : MonoBehaviour
    {

        //once true, enemy will dissapear, no need to worry about setting it back to false
        //Run once for the audio event to be fired
        private bool isVisible = false;
        private bool runOnce = true;

        public float viewRadius;
        [Range(0, 360)]
        public float viewAngle;

        public LayerMask targetMask;
        public LayerMask obstacleMask;

        [HideInInspector]
        public List<Transform> visibleTargets = new List<Transform>();
        private EnemyAiV2 enemyAiScript;
        private Collider2D col;
        void Start()
        {
            enemyAiScript = GetComponentInParent<EnemyAiV2>();
            col = GetComponentInParent<Collider2D>();
            //.2 sec so that they don't see you immediately
            StartCoroutine("FindTargetsWithDelay", .2f);
        }

        IEnumerator FindTargetsWithDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                if (!Controller2D.Instance.isInvisible) //if not invisible
                {
                    FindVisibleTargets2D();
                }
            }
        }

        void Update()
        {
            if (isVisible && !runOnce)
            {
                col.enabled = false; //disable colliders
                enemyAiScript.enabled = false;//disable movement script
                transform.parent.Translate(Vector2.right); //Move the ai away from the screen, goes opposite from the player
                                                           // Debug.Log(Vector2.Distance(this.transform.position, Player.Instance.transform.position));

                //Check the distance in order to destroy the gameObject
                if (Vector2.Distance(this.transform.position, Player.PlayerController.Instance.transform.position) > 100)
                {
                    Destroy(transform.parent.gameObject);
                }
            }
        }

        //Checks if they are visible Targets 3D
        void FindVisibleTargets()
        {
            visibleTargets.Clear();
            Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            for (int i = 0; i < targetInViewRadius.Length; i++)
            {
                Transform target = targetInViewRadius[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                    {
                        visibleTargets.Add(target);
                    }
                }
            }
        }

        //Checks if they are visible Targets 2D
        void FindVisibleTargets2D()
        {
            visibleTargets.Clear();
            //returns target's collider that overlap the sphere
            Collider2D[] targetInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

            //Loop through all targets
            for (int i = 0; i < targetInViewRadius.Length; i++)
            {
                Transform target = targetInViewRadius[i].transform; //Transform of the target
                Vector3 dirToTarget = (target.position - transform.position).normalized; //magnitude

                if (Vector3.Angle(-transform.right, dirToTarget) < viewAngle / 2)
                {
                    float distToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics2D.Raycast(transform.position, dirToTarget, distToTarget, obstacleMask))
                    {
                        if (runOnce)
                        {
                            isVisible = true;
                            runOnce = false;
                            Debug.Log("Visible: " + isVisible);
                            AudioEventSystem.EnemyAlert();
                        }
                        visibleTargets.Add(target);
                    }
                }
            }
        }

        //change to vector2 3D
        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y; //y
            }
            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        //change to vector2 2D
        public Vector3 DirFromAngle2D(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees -= transform.eulerAngles.y; //y
            }
            return new Vector3(-Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), -Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0);
        }
    }
}
