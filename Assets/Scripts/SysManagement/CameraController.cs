using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;

    Vector3 velocity = Vector3.zero;
    public float minX, maxX;
    public float minY, maxY;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = player.position;
        mousePos = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -100f));
        targetPos = new Vector3(Mathf.Clamp(player.position.x, minX, maxX), Mathf.Clamp(player.position.y, minY, maxY), -100f);

        Vector3 difference = mousePos - targetPos;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos+ difference*(0.1f), ref velocity, smoothSpeed);
        
    }
}
