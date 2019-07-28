using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleBulletHit : MonoBehaviour
{
    public Bullet bulletType;
    public frenchFryController ffc;

    public void OnTriggerEnter(Collider col) {
        if(bulletManager.HitTrigger(bulletType, col))
        {
            if(ffc != null)
            {
                ffc.DeleteFry(gameObject);
            }
            bulletManager.GenerateHitEffect(gameObject);
            Destroy(gameObject);
        }
    }
}
