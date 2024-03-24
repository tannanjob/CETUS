using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class FollowPlayer : MonoBehaviour
{
    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    public bool LookAtPlayer = false;
    [SerializeField] float RotationsSpeedH = 0.2f;
    [SerializeField] float RotationsSpeedV = 0.2f;
    [SerializeField] float CameraPitchMin = 0.5f;
    [SerializeField] float CameraPitchMax = 10.0f;
    [SerializeField] Vector3 lookOffset;

    Vector3 shakePos = Vector3.zero;

    float h;
    float v;

    public Vector2 inputDir;
    public float offsetMag = 1;

    float optionalSensitivity = 1;

    //debug
    [SerializeField] GameObject rightStick;

    // Use this for initialization
    void Start()
    {
        //カメラとプレイヤーの距離
        _cameraOffset = transform.position - PlayerTransform.position;
        optionalSensitivity = SoundSetting.UserSens;
    }

    void LateUpdate()
    {
        //カメラ回転　cameraOffsetを算出
        Quaternion camTurnAngle = Quaternion.AngleAxis(h, Vector3.up);
        Quaternion camTurnAngleY = Quaternion.AngleAxis(v, transform.right);
        Vector3 newCameraOffset = camTurnAngle * camTurnAngleY * _cameraOffset;

        // Limit camera pitch
        if (newCameraOffset.y < CameraPitchMin || newCameraOffset.y > CameraPitchMax)
        {
            newCameraOffset = camTurnAngle * _cameraOffset;
        }

        _cameraOffset = newCameraOffset;

        //カメラ位置
        Vector3 newPos = PlayerTransform.position + _cameraOffset * offsetMag + shakePos;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        transform.LookAt(PlayerTransform.position + lookOffset + shakePos);
    }

    //スティックの入力
    public void Look(InputAction.CallbackContext context)
    {
        inputDir = context.ReadValue<Vector2>();

        h = inputDir.x * RotationsSpeedH * (optionalSensitivity + 0.1f);
        v = -inputDir.y * RotationsSpeedV * (optionalSensitivity + 0.1f);

        rightStick.transform.localPosition = inputDir * 0.1f;
    }

    //カメラ揺らす
    public IEnumerator Shake(float duration){
        float shakeMag = 0.5f;
        float interval = 0.05f;
        for(float t = 0;t < duration;t += interval){
            shakeMag *= 0.9f;
            shakePos = (Camera.main.transform.up * Random.Range(-1.0f, 1.0f) + Camera.main.transform.right * Random.Range(-0.20f, 0.20f)) * shakeMag;

            yield return new WaitForSeconds(interval);
        }
        shakePos = Vector3.zero;
    }

    //ポーズ明けに設定反映
    public void Resume(){
        optionalSensitivity = SoundSetting.UserSens; 
    }
}
