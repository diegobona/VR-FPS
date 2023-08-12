using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using BNG; //VRIF

public class Rifle : MonoBehaviour
{
    [Header("Rifle")]
    //public Camera cam; //���Ҫ�ã���CenterEyeAnchor��ֵ����
    public float giveDamage = 50f;
    public float shootingRange = 100f;


    public float triggerThreshold = 0.5f;  // �ֱ���ѹ��ֵ

    public Transform rayStartPoint;

    //Grabber��VRIF�е����ͣ�ץȡ�����������ֿ���������Ҫ��inspector����CameraRig-...-LeftController�����Grabber��ȥ��ֵ��
    public Grabber lHandGrabber;
    public Grabber rHandGrabber;

    void Start()
    {

    }

    
  
    void Update()
    {
        Debug.Log("object currently held in lHand:" + lHandGrabber.HeldGrabbable);//��object����
        Debug.Log("object currently held in rHand:" + rHandGrabber.HeldGrabbable);

        // �����grip�����塢�Լ�Trigger��ť�İ���
        //HeldGrabbable��VRIF�ж�������ԣ�ָ��ǰ����ץ�ŵ�����
        if ((lHandGrabber.HeldGrabbable.tag == "Gun" && InputBridge.Instance.LeftTriggerDown) || (rHandGrabber.HeldGrabbable.tag == "Gun" && InputBridge.Instance.RightTriggerDown))
        {
            Debug.Log("Trigger is holddown");
            Shoot();
        } 

    }

    void Shoot()
    {
        RaycastHit hitInfo;

        //if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hitInfo, shootingRange))
        //{
        //    Debug.Log(hitInfo.transform.name);
        //}

        if (Physics.Raycast(rayStartPoint.position, rayStartPoint.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);
        }

        Objects objects= hitInfo.transform.GetComponent<Objects>();

        if(objects != null)
        {
            objects.objectHitDamage(giveDamage);
        }


    }




}
