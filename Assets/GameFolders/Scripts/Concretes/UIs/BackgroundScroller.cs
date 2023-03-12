using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Space.UIs
{
    public class BackgroundScroller : MonoBehaviour
    {
        [Range(-1f, 1f)]
        [SerializeField] float _scrollSpeed = 0.5f;
        private float _offset;
        private Material _material;

        private void Start()
        {
            _material = GetComponent<Renderer>().material;
        }

        private void Update()
        {
            _offset += (Time.deltaTime * _scrollSpeed) / 10f;
            _material.SetTextureOffset("_MainTex", new Vector2(0, _offset));
        }
    }

}
