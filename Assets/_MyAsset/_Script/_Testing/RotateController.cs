using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject longAngle;
    [SerializeField] GameObject constellationRotate;
    [SerializeField] Dropdown dpMonth, dpDay, dpTime;
    [SerializeField] InputField txtLat, txtlong;

    bool isSet = false;
    void SetStart()
    {
        //calculation for the date is 360 days plus time current time
        //constellationRotate.transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);

        
        

        string  Year = DateTime.Now.Year.ToString();
        string Month = DateTime.Now.Month.ToString();
        string Day = DateTime.Now.Day.ToString();

        int Y = int.Parse(Year);
        int M = int.Parse(Month);
        int D = int.Parse(Day);

        print("resultDay: Y"+ Y + " M: "+M +" D: "+D );
        int DaysLeft = ((13 - M) * 30 ) - D;

        int totalDayPass = 360 - DaysLeft;
        print("totalDayPass: "+ totalDayPass);

        //calculate for the time degree
        string currentHourString = DateTime.Now.Hour.ToString();
        int currentHour = int.Parse(currentHourString);
        int timeDegreeCalcu = 15 * currentHour;


        print("timeDegreeCalcu: " + timeDegreeCalcu);


        int latitude = (int)TestLocationService.latitude;
        int TotalDegrees = timeDegreeCalcu + totalDayPass + latitude;

        print("RotateController: latitude: " + latitude);
        print("RotateController: TotalDegrees: " + TotalDegrees);
        print("RotateController: constellationRotate.transform.rotation.y: " + constellationRotate.transform.rotation.y);


        longAngle.transform.eulerAngles = new Vector3(0, 0, 0);
        //constellationRotate.transform.eulerAngles = new Vector3(0, -TotalDegrees, 0);
        constellationRotate.transform.rotation = Quaternion.AngleAxis(-TotalDegrees, Vector3.up);
        //constellationRotate.transform.Rotate(0, 0, 0);
        //constellationRotate.transform.Rotate(0, -TotalDegrees, 0);
        //constellationRotate.transform.rotation = Quaternion.Euler(0, 90, 0);

        //Debug.Log(Month.options[Month.value].text);


    }


    // Update is called once per frame
    void Update()
    {
        if (isSet == false && TestLocationService.latitude != 0 && TestLocationService.longitude != 0)
        {
            print("RotateController: SetStart: " );
            isSet = true;
            SetStart();
            longAngle.transform.eulerAngles = new Vector3(TestLocationService.longitude, longAngle.transform.rotation.y, longAngle.transform.rotation.z);
        }

       
        constellationRotate.transform.Rotate(new Vector3(0, -0.1f, 0) * Time.deltaTime);
        //constellationRotate.transform.eulerAngles = new Vector3(0, 0.1f, 0);
    }

    public void DropDownMonth()
    {
        print("Dropdown" + dpMonth.options[dpMonth.value].text + "");
     
    }

    public void DropDownDay()
    {
        print("Dropdown" + dpDay.options[dpDay.value].text + "");
    }

    public void DropDownTime()
    {
        print("Dropdown" + dpTime.options[dpTime.value].text + "");
    }

    public void Reload()
    {
        string strLat = txtLat.text+"";
        string strLong = txtlong.text + "";

        int intLong, intLat;

        //Debug.Log("intLat is the number:strLat " + strLat);
        if (int.TryParse(strLat, out intLat))
        {
            Debug.Log("intLat is the number: " + intLat);
            TestLocationService.latitude = intLat;
        }
        else
        {
            Debug.Log("intLat is the number:  No Data");
        }

        if (int.TryParse(strLong, out intLong))
        {
            Debug.Log("intLong is the number: " + intLong);
            TestLocationService.longitude = intLong;
        }
        else
        {
            Debug.Log("intLong is the number:  No Data");
        }


        try
        {
            //constellationRotate.transform.eulerAngles = new Vector3(0, 0 , 0);

            int newMonth = int.Parse(dpMonth.options[dpMonth.value].text);
            int newDay = int.Parse(dpDay.options[dpDay.value].text);
            int newTime = int.Parse(dpTime.options[dpTime.value].text);



            print("Reload: Month" + newMonth + " Day: " + newDay + " Time: " + newTime);

            if (newDay != 0 && newMonth != 0 && newTime != 0)
            {


                
            }

            

            /*if (int.TryParse(txtlong.ToString(), out intLong))
            {
                Debug.Log("intLong is the number: " + intLong);
            }*/


            /*intLong = int.Parse(txtlong.ToString());
            TestLocationService.longitude = intLong;

            intLat = int.Parse(txtLat.ToString());
            TestLocationService.latitude = intLat;*/

            //print("resultDay: Y" + Y + " M: " + M + " D: " + D);
            int DaysLeft = ((13 - newMonth) * 30) - newDay;

            int totalDayPass = 360 - DaysLeft;
            print("totalDayPass: " + totalDayPass);

            //calculate for the time degree
            int timeDegreeCalcu = 15 * newTime;


            print("timeDegreeCalcu: " + timeDegreeCalcu);

            int latitude = (int)TestLocationService.latitude;
            print("latitude: " + latitude);
            int TotalDegrees = timeDegreeCalcu + totalDayPass + latitude;

            //constellationRotate.transform.eulerAngles = new Vector3(0, -TotalDegrees, 0);
            longAngle.transform.eulerAngles = new Vector3(0, 0, 0);
            //constellationRotate.transform.eulerAngles = new Vector3(0, -TotalDegrees, 0);
            constellationRotate.transform.rotation = Quaternion.AngleAxis(-TotalDegrees, Vector3.up);
            longAngle.transform.eulerAngles = new Vector3(TestLocationService.longitude, longAngle.transform.rotation.y, longAngle.transform.rotation.z);


        }
        catch (Exception e)
        {
            print("NO Data: "+e);
        }

        //SetStart();
        //longAngle.transform.eulerAngles = new Vector3(TestLocationService.longitude, longAngle.transform.rotation.y, longAngle.transform.rotation.z);

    }

}