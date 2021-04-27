using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creativeButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //https://www.youtube.com/watch?v=m1lBHP5lxeY
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
    }

}
