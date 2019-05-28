using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NeRemNem.Tools
{
    public class FixCameraToTarget : MonoBehaviour
    {

        public float offsetX = 0f;
        public float offsetY = 10f;
        public float offsetZ = -10f;
        public GameObject target;
        private Vector3 CameraPosition;

        private void OnEnable()
        {
            target = GameObject.FindWithTag("Player");
        }
        private void LateUpdate()
        {
            this.transform.position = SetCamera();
        }
        public Vector3 SetCamera()
        {
            CameraPosition.x = target.transform.position.x + offsetX;
            CameraPosition.y = target.transform.position.y + offsetY;
            CameraPosition.z = target.transform.position.z + offsetZ;
            return CameraPosition;
        }
    }
}
