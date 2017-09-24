using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControl : MonoBehaviour {
    private void OnBecameInvisible()
    {
        this.gameObject.Recycle();
    }
}
