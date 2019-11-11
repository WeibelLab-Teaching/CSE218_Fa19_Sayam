using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWhenGone : MonoBehaviour
{

    [Tooltip("How far down can it go before getting destroyed")]
    public float maxDistance = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -1*maxDistance)
        {
            Destroy(this.gameObject);
        }   
    }
}
