using System;
using UnityEngine;
using UnityStandardAssets.Utility;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class HeadBob : MonoBehaviour
    {
        public Camera Camera;
        public CurveControlledBob motionBob = new CurveControlledBob();
        public LerpControlledBob jumpAndLandingBob = new LerpControlledBob();
        //public RigidbodyFirstPersonController rigidbodyFirstPersonController;
        public TitanFPSController titanFPSController;
        public float StrideInterval;
        [Range(0f, 1f)] public float RunningStrideLengthen;

       // private CameraRefocus m_CameraRefocus;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;


        private void Start()
        {
            motionBob.Setup(Camera, StrideInterval);
            m_OriginalCameraPosition = Camera.transform.localPosition;
       //     m_CameraRefocus = new CameraRefocus(Camera, transform.root.transform, Camera.transform.localPosition);
        }


        private void Update()
        {
          //  m_CameraRefocus.GetFocusPoint();
            Vector3 newCameraPosition = Vector3.zero;
            if (titanFPSController.Velocity.magnitude > 0 && titanFPSController.Grounded)
            {
                Camera.transform.localPosition = Vector3.Lerp(Camera.transform.localPosition,
                                                 motionBob.DoHeadBob(titanFPSController.Velocity.magnitude * (titanFPSController.Running ? RunningStrideLengthen : 1f)),
                                                 Time.deltaTime * 6f);
                
                //motionBob.DoHeadBob(titanFPSController.Velocity.magnitude*(titanFPSController.Running ? RunningStrideLengthen : 1f));
                newCameraPosition = Camera.transform.localPosition;
                newCameraPosition.y = Camera.transform.localPosition.y - jumpAndLandingBob.Offset();
            }
            else
            {
                newCameraPosition = Vector3.Lerp(newCameraPosition, Camera.transform.localPosition, Time.deltaTime *6f);
                newCameraPosition.y = m_OriginalCameraPosition.y - jumpAndLandingBob.Offset();
            }
            Camera.transform.localPosition = Vector3.Lerp(Camera.transform.localPosition, newCameraPosition, Time.deltaTime * 6f);

            if (!m_PreviouslyGrounded && titanFPSController.Grounded)
            {
                StartCoroutine(jumpAndLandingBob.DoBobCycle());
            }

            m_PreviouslyGrounded = titanFPSController.Grounded;
          //  m_CameraRefocus.SetFocusPoint();
        }
    }
}
