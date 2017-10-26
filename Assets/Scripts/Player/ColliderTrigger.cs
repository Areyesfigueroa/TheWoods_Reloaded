using UnityEngine;
using System.Collections;

namespace TheWoods.Player
{
    public class ColliderTrigger : MonoBehaviour
    {

        Bounds triggerBound;
        [Space(5)]
        [Header("Player Object")]
        GameObject playerObj;
        [Space(5)]
        [Header("Trigger Custimization")]
        public Vector3 triggerPosition; //offset
        public Vector3 triggerExpansion;

        //trigger expansion - 
        void Start()
        {
            playerObj = GameObject.FindWithTag("Player");
            triggerBound.extents = new Vector3(1, 1, 1);
        }

        void Update()
        {
            updateTrigger();
            if (playerObj != null)
            {
                //playerCollisionTrigger();
            }
        }

        void updateTrigger()
        {
            triggerBound.center = this.transform.position + triggerPosition;
            triggerBound.extents = triggerExpansion;
        }

        void playerCollisionTrigger()
        {
            if (triggerBound.Intersects(playerObj.GetComponent<Controller2D>().Collider.bounds))
            {
                print("Intersecting with Player");
                playerObj.transform.parent = this.transform;

            }
            else
            {
                playerObj.transform.parent = null;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawCube(triggerBound.center, triggerBound.size);
        }
    }
}
