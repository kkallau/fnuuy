using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public bool IsSecret;
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSecret)
		{
			transform.Rotate(new Vector3(0, 0, 60) * Time.deltaTime);
		}
		else
		{
			transform.Rotate(new Vector3(60, 0, 0) * Time.deltaTime);
		}
    }
}
