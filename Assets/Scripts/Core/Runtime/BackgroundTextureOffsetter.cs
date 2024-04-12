using UnityEngine;

namespace Core.Runtime
{
    public class BackgroundTextureOffsetter : MonoBehaviour
    {
        [SerializeField]
        private Transform _cameraTransform;

        [SerializeField]
        public float _scrollSensitivity = 0.1f;

        private Renderer _rend;
        private static readonly int _mainTex = Shader.PropertyToID("_BaseMap");


        private void Start()
        {
            _rend = GetComponent<Renderer>();
        }

        private void Update()
        {
            var cameraPos = _cameraTransform.position;
            float offsetX = Mathf.Repeat(cameraPos.x * _scrollSensitivity, 1f);
            float offsetY = Mathf.Repeat(cameraPos.z * _scrollSensitivity, 1f);

            _rend.material.SetTextureOffset(_mainTex, new Vector2(offsetX, offsetY));
        }
    }
}