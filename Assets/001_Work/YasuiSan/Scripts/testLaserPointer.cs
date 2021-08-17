using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLaserPointer : MonoBehaviour
{
    #region InitializeValues
    public Transform _RightHandAnchor;
    //public Transform _LeftHandAnchor;
    public Transform _CenterEyeAnchor;
    public float _MaxDistance = 100.0f;
    private float _MinDistance = 0.0f;
    public LineRenderer _LaserPointerRenderer;

    public Transform activeController;

    
    //public GameObject obj = hitInfo.collider.gameObject;
    #endregion
    
    //
    #region �R���g���[���̓��͔���
    /*
    private Transform Pointer
    {
        get
        {
            // ���݂̃A�N�e�B�u�ȃR���g���[�����擾
            //var controller = OVRInput.GetActiveController();

            //if (controller == OVRInput.Controller.RTouch)
            if (OVRInput.GetActiveController() == OVRInput.Controller.RTouch && OVRInput.GetActiveController() == OVRInput.Controller.LTouch)
            {
                // �����F�����Ă��鎞�́A�E����R���g���[���Ƃ���
                activeController = _RightHandAnchor;
            }
            else
            {
                if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
                {
                    activeController = _RightHandAnchor;
                }
                //else if (controller == OVRInput.Controller.LTouch)
                else if (OVRInput.Get(OVRInput.RawButton.LHandTrigger))
                {
                    activeController = _LeftHandAnchor;
                }
                else
                {
                    // �ǂ�����F�����Ă��Ȃ���� �E����R���g���[���Ƃ���
                    activeController = _RightHandAnchor;
                }
            }

            return activeController;
        }
        set { Pointer = activeController; }
    }*/
    #endregion
    
    void Update()
    {
        //var pointer = Pointer;

        //
        #region pointer �G���[�������(null)
        /*
        if (pointer == null || _LaserPointerRenderer == null)
        {
            return;
        }
        */
        #endregion

        // default == (int) 0     !=     null -> (bool) false
        RaycastHit hitInf = default(RaycastHit);
        RaycastHit hitInfo = hitInf;
        //GameObject obj = hitInfo.collider.gameObject;
        //Vector3 maxScale = obj.transform.localScale;
        
        // �R���g���[���̃g���K�[
        //if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            // �R���g���[���̈ʒu����Ray���΂�
            Ray pointerRay = new Ray(_RightHandAnchor.position, _RightHandAnchor.forward);

            // ���[�U�[�̋N�_
            _LaserPointerRenderer.SetPosition(0, pointerRay.origin);

            if (Physics.Raycast(pointerRay, out hitInfo, _MaxDistance))
            {
                // Ray���q�b�g�����炻���܂�
                _LaserPointerRenderer.SetPosition(1, hitInfo.point);

            }
            else
            {
                // Ray���q�b�g���Ȃ�����������Ă������MaxDistance���L�΂�
                _LaserPointerRenderer.SetPosition(1, pointerRay.origin + pointerRay.direction * _MaxDistance);
            }

            // �E��IndexTrigger����������A�I�u�W�F�N�g����]����
            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
            {
                //obj.transform.GetComponent<Rigidbody>().AddTorque(new Vector3(0f,1f,5f));
            }



        }
        else
        {
            // �R���g���[���̈ʒu����Ray ��΂�
            Ray pointerRay = new Ray(_RightHandAnchor.position, _RightHandAnchor.forward);

            // ���[�U�[�̋N�_
            _LaserPointerRenderer.SetPosition(0, pointerRay.origin);

            //�R���g���[���̃g���K�[�����i�g���K�[�������Ă��Ȃ��Ƃ��j�̏ꍇ�ARay�������Ă������MinDistance (0)
            // Ray�̃S�[�X�g�r������
            _LaserPointerRenderer.SetPosition(1, pointerRay.origin + pointerRay.direction * _MinDistance);
        }
    }
}
