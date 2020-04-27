﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ControllerExperiment
{
    public class TempRagdollTransformSetter : MonoBehaviour
    {
        public bool KinematicMovement;
        public bool DoNotSync;
        
        public GameObject targetJoint;
        public float PositionSyncSpeed;
        public float RotationSyncSpeed;

        Rigidbody myRigidBody;
        ConfigurableJoint myJoint;

        private void Start()
        {
            myRigidBody = this.gameObject.GetComponent<Rigidbody>();
            myJoint = this.gameObject.GetComponent<ConfigurableJoint>();
        }

        private void FixedUpdate()
        {
            myRigidBody.isKinematic = KinematicMovement;

            if (KinematicMovement)
            {
                if (!DoNotSync)
                {
                    Quaternion targetRotation = Quaternion.Lerp(myJoint.transform.rotation, targetJoint.transform.rotation, Time.deltaTime * RotationSyncSpeed);
                    myRigidBody.MoveRotation(targetRotation);

                    Vector3 targetPosition = Vector3.Lerp(myJoint.transform.position, GetMyAnchorPosition(), Time.deltaTime * PositionSyncSpeed);
                    myRigidBody.MovePosition(targetPosition);

                    Debug.DrawLine(Vector3.zero, GetMyAnchorPosition(), Color.red, 0.5f);
                }
            }
            else
            {
                // still testing
            }
        }

        Vector3 GetMyAnchorPosition()
        {
            if (myJoint.connectedBody == null)
            {
                return Vector3.zero;
            }
            else
            {
                Vector3 myAnchor = myJoint.connectedBody.position + myJoint.connectedAnchor;

                return myAnchor;
            }
        }
    }
}