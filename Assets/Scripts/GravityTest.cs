using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Death
{
    public class GravityTest : MonoBehaviour
    {
        public static bool onStairs;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
                onStairs = true;
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
                onStairs = false;
        }
    }
}
