using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private float duration = 0;
    // Start is called before the first frame update
    /*    void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }*/

    void OnTriggerStay2D(Collider2D other){

        duration += Time.deltaTime;
        if(duration >= 1) {
            duration = 0f;
            Debug.Log(" *********************  " + other);
        }

    }

    void OnTriggerEnter2D(Collider2D other) {

        RubyControl controller = other.GetComponent<RubyControl>();
        if (controller != null) {
            controller.ChangeHealth(-1);
        }
    
    }
}
