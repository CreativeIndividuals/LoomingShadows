using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraScript : MonoBehaviour
{
    [SerializeField] Transform Player;
    Vector2 mousePos;
    Camera cam;
    [Space]
    [SerializeField] float FollowStrength;
    [SerializeField] float MaxRange;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - new Vector2(Player.position.x, Player.position.y);

        direction = Vector3.ClampMagnitude(direction, MaxRange);

        transform.position = new Vector3(Player.transform.position.x + (direction.x * FollowStrength), Player.transform.position.y + (direction.y * FollowStrength), -10f);
    }
}
