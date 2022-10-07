using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    RaycastHit HitInfo;

    [SerializeField] private List<GameObject> constilation = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }


    // Update is called once per frame
    void Update()
    {
        Transform cameraTransform = Camera.main.transform;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 3000.0f))
        {
            int index = int.Parse(HitInfo.collider.gameObject.name);
            //Debug.Log("HitInfo: "+ HitInfo.collider.gameObject.name);
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.yellow);

            if (constilation.Count > index)
            {
                GameObject ChildGameObject1 = constilation[index].transform.GetChild(0).gameObject;
                ChildGameObject1.SetActive(true);
            }

            //GameObject ChildGameObject2 = ParentGameObject.transform.GetChild(1).gameObject;
        }
        else
        {
            GameObject ChildGameObject1 = constilation[0].transform.GetChild(0).gameObject;
            ChildGameObject1.SetActive(false);
            GameObject ChildGameObject2 = constilation[1].transform.GetChild(0).gameObject;
            ChildGameObject2.SetActive(false);
            GameObject ChildGameObject3 = constilation[2].transform.GetChild(0).gameObject;
            ChildGameObject3.SetActive(false);
            GameObject ChildGameObject4 = constilation[3].transform.GetChild(0).gameObject;
            ChildGameObject4.SetActive(false);
            GameObject ChildGameObject5 = constilation[4].transform.GetChild(0).gameObject;
            ChildGameObject5.SetActive(false);
            GameObject ChildGameObject6 = constilation[5].transform.GetChild(0).gameObject;
            ChildGameObject5.SetActive(false);
            GameObject ChildGameObject7 = constilation[6].transform.GetChild(0).gameObject;
            ChildGameObject7.SetActive(false);
            GameObject ChildGameObject8 = constilation[7].transform.GetChild(0).gameObject;
            ChildGameObject8.SetActive(false);
            GameObject ChildGameObject9 = constilation[8].transform.GetChild(0).gameObject;
            ChildGameObject9.SetActive(false);
            GameObject ChildGameObject10 = constilation[9].transform.GetChild(0).gameObject;
            ChildGameObject10.SetActive(false);
            GameObject ChildGameObject11 = constilation[10].transform.GetChild(0).gameObject;
            ChildGameObject11.SetActive(false);
            GameObject ChildGameObject12 = constilation[11].transform.GetChild(0).gameObject;
            ChildGameObject12.SetActive(false);
            GameObject ChildGameObject13 = constilation[12].transform.GetChild(0).gameObject;
            ChildGameObject13.SetActive(false);

            ChildGameObject1.SetActive(false);
            ChildGameObject1.SetActive(false);
        }

    }
}
