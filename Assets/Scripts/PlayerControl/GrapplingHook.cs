using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

/*
A Line Renderer which will show the rope.
When the rope wraps around things, you can add more segments
to the line renderer and position the vertices appropriately
around the edges the rope wraps.

A DistanceJoint2D. This can be used to attach to the grappling
hook’s current anchor point, and lets the slug swing. It’ll also
allow for configuration of the distance, which can be used to
rappel up and down the rope.

A child GameObject with a RigidBody2D that can be moved around
depending on the current location of the hook’s anchor point.
This will essentially be the rope hinge / anchor point.

A raycast for firing the hook and attaching to objects.
*/
