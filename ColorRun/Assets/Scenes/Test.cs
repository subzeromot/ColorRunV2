using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Test : MonoBehaviour {
    [SerializeField]
    private Transform targetPoint1;
    [SerializeField]
    private Transform targetPoint2;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
            this.onMoveLeft();
        if (Input.GetKeyUp(KeyCode.RightArrow))
            this.onMoveRight();
    }

    private void onMoveLeft()
    {
        //transform.DOMoveX(this.transform.position.x - 1, 1);
        transform.DOJump(targetPoint1.position, 1f, 3, 1).SetEase(Ease.InQuad);
        
    }

    private void onMoveRight()
    {
        //transform.DOMoveX(this.transform.position.x + 1, 1);
        transform.DOJump(targetPoint2.position, 1f, 3, 1).SetEase(Ease.Linear);
    }
}
