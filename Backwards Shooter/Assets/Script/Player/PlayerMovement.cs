using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    bool canPlay = false;
    public float speedX; Quaternion initRot;
    Vector3 initLocalPos;
    Vector3 transformPos = Vector3.zero;
    Player player;
    void Start()
    {
        initRot = transform.rotation;
        initLocalPos = transform.localPosition;
        GameManager.instance.start += LevelStart;
        GameManager.instance.boundary += SetBoundary;
        player = Player.instance;
    }
    public void LevelStart()
    {
        canPlay = true;
        canShoot = true;
    }

    void Update()
    {
        if (canPlay)
        {
            if (Input.touchCount > 0 || (Input.GetMouseButton(0)))
            {
                MOVEX();
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition,
                    new Vector3(transformPos.x, transform.localPosition.y, transform.localPosition.z),
                    speedX * Time.deltaTime
                    );
            XBoundary();
            updateSpeed();
        }
    }

    #region Shooting 
    public List<Bullet> bullets;
    public GameObject magzine;
    public bool canShoot=false;
    public void Shoot(GameObject obj)
    {
        if (canShoot)
        StartCoroutine(IShoot(obj));
    }
    IEnumerator IShoot(GameObject obj)
    {
        if (bullets.Count > 0)
        {
            canShoot = false;
            Bullet bullet = bullets[0];
            bullet.gameObject.SetActive(true);
            bullets.RemoveAt(0);
            bullet.transform.position = this.transform.position;
            bullet.dir = (obj.transform.position - transform.position).normalized;
            bullet.transform.parent = null;
            yield return new WaitForSeconds(.1f);
            canShoot = true;
            yield return new WaitForSeconds(1.5f);
            bullet.transform.parent = magzine.transform;
            bullet.transform.position = this.transform.position;
            bullets.Add(bullet);
        }
    }

    #endregion



    public bool touchControl = false; public bool dragBegins = false; Vector3 lastmousePos = Vector3.zero;
    private void MOVEX()
    {
        if (dragBegins)
        {
            Vector3 deltaPos = (Input.mousePosition - lastmousePos);
            //Vector3 deltaPos = Input.mouseScrollDelta;
            Vector3 pos = transform.localPosition;
            pos.x += deltaPos.x / Screen.width;
            transformPos.x = pos.x; //= new Vector3(pos.x, transformPos.y, transform.localPosition.z);
            lastmousePos = Input.mousePosition;
        }
    }
    float XAxisBoundary = 0;

    void SetBoundary(float boudary)
    {

        XAxisBoundary = (boudary / 2) - .5f;
    }
    void XBoundary()
    {
        Vector3 pos = transform.localPosition;
        if (pos.x > XAxisBoundary)
        {
            pos.x = XAxisBoundary;
        }
        else if (pos.x < -XAxisBoundary)
        {
            pos.x = -XAxisBoundary;
        }

        if (transform.position.y < 0)
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        transform.localPosition = pos;

    }
    float splineFollowSpeed = 1f;
    float followSpeedRate = .1f;
    void updateSpeed()
    {
        splineFollowSpeed = Mathf.Clamp(splineFollowSpeed, player.speedMin, player.speedMax);
        splineFollowSpeed = Mathf.Lerp(splineFollowSpeed, player.speedMax, followSpeedRate * Time.deltaTime);
        player.SetFollowSpeed(splineFollowSpeed);
    }
    public void OnDrageBegins()
    {
        dragBegins = true;
    }
    public void OnDrageEnds()
    {
        dragBegins = false;
    }
    public void MouseDown()
    {
        lastmousePos = Input.mousePosition;
    }
    public void MouseUp()
    {
        lastmousePos.x = 0;
    }

    void LevelFinish(bool isComplete)
    {
        canPlay = false;
        GameManager.instance.LevelFinish(isComplete);
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("hurdle"))
        {
            splineFollowSpeed = player.speedMin;
            Debug.LogError("hurdle");
        }
        else if (col.gameObject.CompareTag("finish"))
        {
            LevelFinish(true);
        }
        else if (col.gameObject.CompareTag("enemy"))
        {
            LevelFinish(false);
        }
    }
}
