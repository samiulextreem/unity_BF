using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnlandCameraShake : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerLandingCheck plyLandChk;
    private CinemachineVirtualCamera cinemachvirt;
    public float shakeTimer;
    public static OnlandCameraShake instance { get; private set; }
    public float startingIntensity;
    public float shakeTimeTotal;

    //Inspector field for the Shaker component.
    private void Awake()
    {
        instance = this;
        cinemachvirt = GetComponent<CinemachineVirtualCamera>();
    }



    void Start()
    {
        
        plyLandChk = GameObject.Find("player").GetComponent<PlayerLandingCheck>();
        
        

    }

    // Update is called once per frame
    void Update()
    { 
        
            
        if(shakeTimer > 0)
        {
            shakeTimer = shakeTimer - Time.deltaTime;
            if(shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemacMultperln =
                cinemachvirt.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemacMultperln.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0, (1 - (shakeTimer / shakeTimeTotal)));

            }
        
       

        }


    }

    public void shakeCamera(float intensity,float time)
    {
        startingIntensity = intensity;
        CinemachineBasicMultiChannelPerlin cinemacMultperln =
            cinemachvirt.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemacMultperln.m_AmplitudeGain = intensity;
        shakeTimeTotal = time;
        shakeTimer = time;
    }
}
