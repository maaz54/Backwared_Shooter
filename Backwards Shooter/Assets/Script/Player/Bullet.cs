using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Vector3 dir;
    public float speed;
    private void Update()
    {
        transform.Translate(dir *speed*Time.deltaTime);
    }
}
