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

    //Grabber��VRIF�е����ͣ�ץȡ�����������ֿ���������Ҫ��inspector����CameraRig-...-LeftController�����Grabber��ȥ��ֵ��
    public Grabber lHandGrabber;
    public Grabber rHandGrabber;

    public float triggerThreshold = 0.5f;  // �ֱ���ѹ��ֵ.��ʱδ�õ�

    public Transform rayStartPoint;

    public GameObject woodEffect;
    public GameObject metalEffect;
    public GameObject humanEffect;
    public GameObject concreteEffect;





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
            // ������߼�⵽��ײ������һ����ɫ����
            Debug.DrawRay(rayStartPoint.position, rayStartPoint.forward* hitInfo.distance, Color.red);


            if (hitInfo.transform.tag == "Metal")
            {
                Debug.Log("hit a Metal!!!!!!!");
                GameObject metalGo = Instantiate(metalEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(metalGo, 2f);
            }
            else if (hitInfo.transform.tag == "Enemy")
            {
                Debug.Log("hit a enemy!!!!!!!");
                GameObject humanGo = Instantiate(humanEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(humanGo, 2f);
            }
            else if (hitInfo.transform.tag == "Wood")
            {
                Debug.Log("hit a wood!!!!!!!");
                GameObject woodGo = Instantiate(woodEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(woodGo, 2f);
            }

            Objects objects = hitInfo.transform.GetComponent<Objects>();

            if (objects != null)
            {
                objects.objectHitDamage(giveDamage);

               
            }
        }

       


    }




}
