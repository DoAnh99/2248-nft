using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveBlock2 : MonoBehaviour
{

    [SerializeField] private float speed = 5f;

    void Update()
    {
       
            if (transform.parent != null) // candy's has a parent
            {
                if (transform.localPosition == Vector3.zero) // if localPosition is zero just return 
                {
                    return;
                }
                else
                {
                    // move to parent object(grid)
                    float x = transform.position.x - transform.parent.transform.position.x;
                    float y = transform.position.y - transform.parent.transform.position.y;
                    transform.DOMove(new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y, 100f), 0.3f);
                }
            }
        
    }
}
