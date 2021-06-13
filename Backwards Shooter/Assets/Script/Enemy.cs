using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public float Xspeed;
    public float Zspeed;
    bool canplay = false;
    bool otherEnemyinFront = false;

    private void Start()
    {
        GameManager.instance.levelFinish += LevelFinish;
        GameManager.instance.start += GameStart;
        GameManager.instance.boundary += SetBoundary;
        target = Player.instance.transform.GetChild(0);
    }
    private void Update()
    {
        if (canplay)
        {
            Vector3 targetpos = transform.position;
            targetpos.x = target.position.x;
            targetpos.z = target.position.z;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
            {
                if (hit.transform.gameObject.CompareTag("hurdle") || hit.transform.gameObject.CompareTag("enemy"))
                {
                    //transform.Translate(Vector3.right * Xspeed * moveDirec * Time.deltaTime);
                    targetpos.x = transform.position.x +(5*moveDirec);
                }

            }
            //else
            //{
            //    if (target && !otherEnemyinFront)
            //    {
            //        //Vector3 targetpos = target.position;
            //        //Vector3 targetpos = transform.position;
            //        //targetpos.x = target.position.x;
            //        //targetpos.z = target.position.z;
            //    }
            //}
            //Zspeed = Mathf.Lerp(Zspeed, Random.Range(Zspeed, Zspeed + 2),.1f*Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetpos, Zspeed * Time.deltaTime);
            if (XAxisBoundary != 0)
                Boundary();
        }
    }

    int moveDirec = 1;


    float XAxisBoundary = 0;


    void SetBoundary(float boudary)
    {
        Debug.LogError("SetBoundary");
        XAxisBoundary = (boudary / 2) - .5f;

        rb.velocity = Vector3.zero;

    }
    void Boundary()
    {
        Vector3 pos = transform.position;
        if (pos.x > XAxisBoundary)
        {
            pos.x = XAxisBoundary;//right
            moveDirec = -1;
        }
        else if (pos.x < -XAxisBoundary)
        {
            pos.x = -XAxisBoundary;//left
            moveDirec = 1;
        }

        if (pos.y < .5f)
        {
            pos.y = .5f;
        }
        transform.position = pos;
        //if (rb.velocity.z < 0  || rb.velocity.z > 0)
        //{
        //    Vector3 vel = rb.velocity;
        //    vel.z = 0;
        //    rb.velocity = vel;
        //}

    }

    void GameStart()
    {
        canplay = true;
    }
    void LevelFinish(bool isfinish)
    {
        canplay = false;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.CompareTag("bullet"))
        {
            col.gameObject.SetActive(false);
            GameManager.instance.EnemyDead();
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        GameManager.instance.levelFinish -= LevelFinish;
        GameManager.instance.start -= GameStart;
        GameManager.instance.boundary -= SetBoundary;
    }
}
