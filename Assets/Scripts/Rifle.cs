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
    //public Camera cam;   //���Ҫ�ã���CenterEyeAnchor�Ͻ�ȥ��ֵ����
    public float giveDamage = 50f; //���к�ļ�Ѫ�������������õ����ȼ���
    public float shootingRange = 100f;

    //Grabber���ͣ�VRIF�е����ͣ�ץȡ�����������ֿ���������Ҫ��inspector����CameraRig-...-LeftController�����Grabber��ȥ��ֵ��
    public Grabber lHandGrabber;
    public Grabber rHandGrabber;

    public float triggerThreshold = 0.5f;  // �ֱ�trigger����ѹ��ֵ����ʱδ�õ�

    public Transform rayStartPoint;

//���岻ͬ�������屻������Ч��(��ֵ���ڽ���inspector�а���Ӧ��prefab�Ͻ�ȥ)��
    public GameObject woodEffect;
    public GameObject metalEffect;
    public GameObject humanEffect;
    public GameObject concreteEffect;




    void Start()
    {

    }

    
    void Update()
    {

      
        // ������������ŵ����塢�Լ�Trigger��ť�Ƿ���
        // HeldGrabbable��VRIF�ж�������ԣ�ָ��ǰ�������ŵ�����

        if (lHandGrabber.HeldGrabbable != null)
        {
            Debug.Log("object currently held in lHand:" + lHandGrabber.HeldGrabbable);//���ص���VRIF��Grabbable���ͣ�����GameObject�����������壺�ɱ�ץȡ�ġ�
            if (lHandGrabber.HeldGrabbable.tag == "Gun" && InputBridge.Instance.LeftTriggerDown)
            {
                if (checkBulletNum() > 0)
                {
                    Shoot();
                }
            }
        }

        if (rHandGrabber.HeldGrabbable != null)
        {
            Debug.Log("object currently held in rHand:" + rHandGrabber.HeldGrabbable);
            if (rHandGrabber.HeldGrabbable.tag == "Gun" && InputBridge.Instance.RightTriggerDown)
            {
                if (checkBulletNum() > 0)
                {
                    Shoot();
                }
            }
        }

        //if ((lHandGrabber.HeldGrabbable.tag == "Gun" && InputBridge.Instance.LeftTriggerDown) || (rHandGrabber.HeldGrabbable.tag == "Gun" && InputBridge.Instance.RightTriggerDown))
        //{
        //    if (checkBulletNum() > 0)
        //    {
        //        Shoot();
        //    }
        //}

    }


//���ǹ���ʣ���ӵ�����:
    int checkBulletNum()
    {
        int bulletNum = gameObject.GetComponent<RaycastWeapon>().GetBulletCount();//����VRIF��RaycastWeapon.cs�ű��е�GetBulletCount������gameObject.�ǵ�ǰ�ű����ص����塣
        Debug.Log("ʣ���ӵ�����" + bulletNum);
        return bulletNum;
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
            Debug.Log("ǹ��raycast hit��������:"+hitInfo.transform.name);

            // ������߼�⵽��ײ������һ����ɫ����
            Debug.DrawRay(rayStartPoint.position, rayStartPoint.forward* hitInfo.distance, Color.red);

            //���в�ͬ���ʵ����壬������ͬ������Ч����
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

            
            Enemy enemy = hitInfo.transform.GetComponent<Enemy>();//��ȡ���е�enemy��Enemy������ű�Ҳ���������

            if(enemy != null)
            {
                enemy.EnemyHitDamage(giveDamage);
            }


            // Objects objects = hitInfo.transform.GetComponent<Objects>();//ͨ����ȡ����ķ�ʽ������һ��Objects.cs�ű���ʵ�����󣨽ű�Ҳ�������

            //if (objects != null)
            //{
            //    //�����е�object����Ҫ���ˣ���������ֵ
            //    objects.objectHitDamage(giveDamage);//����Objects.cs�ű��е�objectHitDamage����
            //}

        }

       


    }




}
