using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RbExtensions 
{
    //used to check when colliding with certain objects, for multiple rigidbodies

    public static bool RaycastRayHit(this Rigidbody2D rigidbody, Vector2 direction, float distance,
        LayerMask layer)
    {
        if (rigidbody.isKinematic) { return false; }

        RaycastHit2D hitRay = Physics2D.Raycast(rigidbody.position, direction.normalized, distance,
            layer);
               
        Vector3 endRay = new Vector3(rigidbody.transform.position.x + (direction.normalized.x * distance),
            rigidbody.transform.position.y + (direction.normalized.y * distance),
            rigidbody.transform.position.z); // used to know endpoint of line drawn, for debugging purposes

        Debug.DrawLine(rigidbody.transform.position, endRay, Color.green); // used to draw a line, for debugging

        if (hitRay.collider != null && hitRay.rigidbody != rigidbody)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
