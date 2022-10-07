using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassController : MonoBehaviour
{
    // Start is called before the first frame update
    RectTransform rectTransform;
    [SerializeField] Text CompassValue;

    public float compassSmooth = 0.5f;
    private float m_lastMagneticHeading = 0f;

    [SerializeField] private GameObject setCamera;
    [SerializeField] private GameObject setDirection;
    bool setLocation = false;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Input.location.Start();
        Input.compass.enabled = true;


    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.Euler(0, -Input.compass.magneticHeading, 0);
        //rectTransform.Rotate(new Vector3(0, 0, -Input.compass.magneticHeading));

        //transform.rotation = Quaternion.Euler(0, 0, -Input.compass.magneticHeading);
        int m_lastMagneticHeading_converted = (int) m_lastMagneticHeading; 
        CompassValue.text = ""+ m_lastMagneticHeading_converted;

        float currentMagneticHeading = (float)Math.Round(Input.compass.magneticHeading, 2);
        if (m_lastMagneticHeading < currentMagneticHeading - compassSmooth || m_lastMagneticHeading > currentMagneticHeading + compassSmooth)
        {
            m_lastMagneticHeading = currentMagneticHeading;
            transform.localRotation = Quaternion.Euler(0, 0, m_lastMagneticHeading);

            if (setLocation == false)
            {
                setLocation = true; 
                setCamera.transform.Rotate(setCamera.transform.rotation.x, m_lastMagneticHeading, setCamera.transform.rotation.z, Space.Self);
                setDirection.transform.Rotate(setCamera.transform.rotation.x, m_lastMagneticHeading, setCamera.transform.rotation.z, Space.Self);
            }


            //setDirection.transform.rotation.eulerAngles.Set(setDirection.transform.rotation.x, m_lastMagneticHeading, setDirection.transform.rotation.z);
            //setCamera.transform.rotation.eulerAngles.Set(setCamera.transform.rotation.x, m_lastMagneticHeading, setCamera.transform.rotation.z);
        }

       
        
    }
}
