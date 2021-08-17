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
    #region コントローラの入力判定
    /*
    private Transform Pointer
    {
        get
        {
            // 現在のアクティブなコントローラを取得
            //var controller = OVRInput.GetActiveController();

            //if (controller == OVRInput.Controller.RTouch)
            if (OVRInput.GetActiveController() == OVRInput.Controller.RTouch && OVRInput.GetActiveController() == OVRInput.Controller.LTouch)
            {
                // 両方認識している時は、右手をコントローラとする
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
                    // どちらも認識していなければ 右手をコントローラとする
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
        #region pointer エラー回避処理(null)
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
        
        // コントローラのトリガー
        //if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            // コントローラの位置からRayを飛ばす
            Ray pointerRay = new Ray(_RightHandAnchor.position, _RightHandAnchor.forward);

            // レーザーの起点
            _LaserPointerRenderer.SetPosition(0, pointerRay.origin);

            if (Physics.Raycast(pointerRay, out hitInfo, _MaxDistance))
            {
                // Rayがヒットしたらそこまで
                _LaserPointerRenderer.SetPosition(1, hitInfo.point);

            }
            else
            {
                // Rayがヒットしなかったら向いてる方向にMaxDistance分伸ばす
                _LaserPointerRenderer.SetPosition(1, pointerRay.origin + pointerRay.direction * _MaxDistance);
            }

            // 右のIndexTriggerを押したら、オブジェクトが回転する
            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger))
            {
                //obj.transform.GetComponent<Rigidbody>().AddTorque(new Vector3(0f,1f,5f));
            }



        }
        else
        {
            // コントローラの位置からRay 飛ばす
            Ray pointerRay = new Ray(_RightHandAnchor.position, _RightHandAnchor.forward);

            // レーザーの起点
            _LaserPointerRenderer.SetPosition(0, pointerRay.origin);

            //コントローラのトリガー無し（トリガーを引いていないとき）の場合、Rayが向いてる方向にMinDistance (0)
            // Rayのゴースト排除処理
            _LaserPointerRenderer.SetPosition(1, pointerRay.origin + pointerRay.direction * _MinDistance);
        }
    }
}
