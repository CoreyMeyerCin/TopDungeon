using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializedField] public Camera mainCamera;
    public Vector3 mouseWorldPosition;

    private Vector2 offset; //mouse position vs player position
    private float angle; //used to make the projectile display correctly
    //public Transform insantiateRotation;
    private void Update()
    {   
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;
        transform.position = mouseWorldPosition;

        /////////////////This part if for projectile rotation and does not effect melee//It is used Attack>>Shoot>>instantiate/////////////
        offset = new Vector2(
            mouseWorldPosition.x- GameManager.instance.player.transform.position.x,
            mouseWorldPosition.y- GameManager.instance.player.transform.position.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;//this gets our rotation with math in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, angle-90f);

        //Debug.LogWarning($"{transform.rotation}");
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}
