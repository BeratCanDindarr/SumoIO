using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraController : MonoBehaviour
{
    public  CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();   
    }

    //Puana bakarak Kammeranýn konumunu ayarlamamýzý saglayan kod

   public void OffSetCamera(float _value)
    {
        var transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();

        if (_value != transposer.m_FollowOffset.z)
        {

            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset,new Vector3(0,10,-_value),Time.deltaTime);

        }
    }
}
