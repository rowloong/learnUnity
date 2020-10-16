using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Debug.Log(horizontal +    "=>" + "32312");
        Vector2 position = transform.position;
        position.x = position.x + 1.1f * horizontal * Time.deltaTime; ;
        position.y = position.y + 1.1f * vertical * Time.deltaTime; ;
        transform.position = position;
    }
}
