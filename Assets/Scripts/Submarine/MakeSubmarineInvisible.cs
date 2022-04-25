using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubNemesis.Submarine
{
    public class MakeSubmarineInvisible : MonoBehaviour
    {
        public void Disappear()
        {
            gameObject.SetActive(false);
        }
    }
}