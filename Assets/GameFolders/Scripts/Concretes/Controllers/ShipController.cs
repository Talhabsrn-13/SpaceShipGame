using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.Controller
{
    public class ShipController : MonoBehaviour
    {
        [SerializeField] GameObject[] _guns;
        public GameObject[] Guns => _guns;

    }
}