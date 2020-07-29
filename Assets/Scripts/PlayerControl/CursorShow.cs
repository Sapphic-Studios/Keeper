using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorShow : MonoBehaviour
{
    private Vector3 target;
    public GameObject pointer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        pointer.transform.position = new Vector2(target.x, target.y);
    }
}
