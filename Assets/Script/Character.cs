using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region VAR
    public GameObject gM;
    public GameObject cCamera;
    private const int V = 0;
    Vector2 move, mouse;
    Rigidbody2D rb;
    private int INext = 0;
    float angle;
    #endregion

    #region Method
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Move(float ad, float ws)
    {
        move.Set(ad, ws);
        move = move.normalized * 5.0f * Time.deltaTime;
        rb.MovePosition(transform.position + new Vector3(move.x, move.y, 0.0f)); ;
    }
    #endregion

    #region Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Wall"))
        {
            Vector2 Direction = transform.position - collision.transform.position;

            gameObject.transform.GetComponent<Rigidbody2D>().AddForce(Direction.normalized * 2.0f);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Floor"))
        {
            gM.GetComponent<MapCreate>().MapDelete(ref INext);
        }
    }
    #endregion

    #region Update
    private void Update()
    {
        cCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
    }

    private void FixedUpdate()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mouse.y - gameObject.transform.position.y, mouse.x - gameObject.transform.position.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        float ad = Input.GetAxisRaw("Horizontal");
        float ws = Input.GetAxisRaw("Vertical");

        Move(ad, ws);
    }
    #endregion

}
