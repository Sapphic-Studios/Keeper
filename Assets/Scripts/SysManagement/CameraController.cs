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
    public Vector3 mousePos;
    public float dist;
    public float value;
    float edgeSize = 3f;
    public float width = Screen.width;
    public float mouseX = Input.mousePosition.x;
    public float offsetX = 8f;
    public float offsetY = 4.7f;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        offsetX = 8f;
        offsetY = 4.7f;
}

    // Update is called once per frame
    void FixedUpdate()
    {
        mouseX = Input.mousePosition.x;
        Vector3 targetPos = player.position;
        mousePos = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -100f));
        targetPos = new Vector3(Mathf.Clamp(player.position.x, minX, maxX), Mathf.Clamp(player.position.y, minY, maxY), -100f);

        Vector3 difference = new Vector3 (mousePos.x - targetPos.x, mousePos.y - targetPos.y,0);
        dist = difference.magnitude;
        value = ((Mathf.Log(dist)) * 0.1f);
        /*
        if (difference.magnitude > 5f) dist = difference.magnitude;
        else dist = 1000f;*/
        if (Input.GetKey("d") || mousePos.x > player.position.x + offsetX)
        {
            targetPos.x += 100f * Time.deltaTime;
        }
        if (Input.GetKey("a") || mousePos.x < player.position.x - offsetX)
        {
            targetPos.x -= 100f * Time.deltaTime;
        }
        if (Input.GetKey("w") || mousePos.y > player.position.y + offsetY)
        {
            targetPos.y += 100f * Time.deltaTime;
        }
        if (Input.GetKey("s") || mousePos.y < player.position.y - offsetY)
        {
            targetPos.y -= 100f * Time.deltaTime;
        }
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothSpeed);
        
        
    }
}
