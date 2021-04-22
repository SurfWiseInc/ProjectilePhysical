using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBullet : MonoBehaviour
{
    private void Update()
    {
       if( Time.frameCount % 5 == 0)
        {
            if (this.transform.position.y < 0)
                this.gameObject.SetActive(false);
        }

    }
    
}
