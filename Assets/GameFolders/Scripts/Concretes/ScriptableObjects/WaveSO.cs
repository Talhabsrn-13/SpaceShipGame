using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveSpawner;

namespace Space.SO
{
    [CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects / New Wave", order = 2)]
    public class WaveSO : ScriptableObject
    {
        public Wave[] _waves;
    }

}
