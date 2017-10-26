using UnityEngine;
using System.Collections;

namespace TheWoods.Manager
{
    public class LevelManager : MonoBehaviour
    {

        //keep track of level instantiation
        public GameObject playerObj;
        public Transform playerStartPos;

        // Use this for initialization
        void Start()
        {
            playerObj.transform.position = playerStartPos.position;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
