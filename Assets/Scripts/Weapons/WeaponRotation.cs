using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    Camera cam;
    Vector3 mousePos;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 VectorToTarget = mousePos - transform.position;
        float angle = Mathf.Atan2(VectorToTarget.y, VectorToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);


        if (angle > -89f && angle < 89f)
            transform.localScale = new Vector2(1f, transform.localScale.y);
        else
            transform.localScale = new Vector2(-1f, transform.localScale.y);
    }
}
