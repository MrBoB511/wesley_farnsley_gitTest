using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightControl : MonoBehaviour
{
    Light light;
    bool goingUp = false;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(lightChange());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator lightChange()
    {
        /*
        while (true)
        {
            if (light.intensity > 0 && !goingUp)
            {
                light.intensity = light.intensity - (1 * Time.deltaTime);
            }
            else
            {
                goingUp = true;
                light.intensity = light.intensity + (1 * Time.deltaTime);
                if (light.intensity > 5)
                {
                    goingUp = false;
                }
            }
            yield return null;
        }
        */
        while (true)
        {
            while(light.intensity > 0)
            {
                light.intensity -= (Time.deltaTime * 2);
                yield return null;
            }
            while(light.intensity < 5)
            {
                light.intensity += (Time.deltaTime * 2);
                yield return null;
            }
        }
    }
}
