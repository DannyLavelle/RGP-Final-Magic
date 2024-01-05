using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class XPMerger : MonoBehaviour
{
    public float mergeDistance = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckMerge", 0f, 1f);
    }


    void CheckMerge()

    {
        //Debug.Log("Mergeing Start");
        GameObject[] xpOrbs = GameObject.FindGameObjectsWithTag("XP");
        //float startTime = Time.realtimeSinceStartup;

        for (int i = 0; i < xpOrbs.Length; i++)
        {
            for (int j = i + 1; j < xpOrbs.Length; j++)
            {
                float distance = Vector3.Distance(xpOrbs[i].transform.position, xpOrbs[j].transform.position);

                if (distance < mergeDistance)
                {
                    MergeXP(xpOrbs[i], xpOrbs[j]);
                }
            }
        }
        //float endTime = Time.realtimeSinceStartup;

        //float mergeTime = endTime - startTime;

        //Debug.Log("Merging End. Time taken: " + mergeTime + " seconds");
    }
    private void MergeXP(GameObject orb1, GameObject orb2)
    {
        // Add your merging logic here
        // For example, you can combine the XP values and destroy one of the orbs
        // You should customize this based on your game's XP merging rules

        float xpValue1 = orb1.GetComponent<XPScript>().value;
        float xpValue2 = orb2.GetComponent<XPScript>().value;

        float mergedXP = xpValue1 + xpValue2;

        // Update the XP value of orb1 or create a new orb with the merged XP value
        orb1.GetComponent<XPScript>().value = mergedXP;

        // Destroy orb2 as it has been merged
        Destroy(orb2);
    }
}
