using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Right now, this only works if player starts at Vec3(0,0,0), which is stupid.
/// TODO -- Configure the code to work no matter where the player starts.
/// </summary>
public class IsometricCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject player;

    [SerializeField]
    Transform player_transform;
  

    [SerializeField]
    float height = 15.0f;
    [SerializeField]
    float distance = 7.0f;

    public float move_smoothness = 2.0f;
    public float rotate_smoothness = 2.0f;
    void Start()
    {
        player_transform = player.GetComponent<Transform>();

        Vector3 newPosition = player_transform.position;
        newPosition.y = height;
        newPosition.x += distance;
        newPosition.z -= distance;
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * move_smoothness);

        /*
        Vector3 lookDirection = player_transform.position - transform.position;
        lookDirection.Normalize();

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotate_smoothness * Time.deltaTime);
        */

    }

    // Update is called once per frame
    void Update()
    {
        if (player_transform != null) {

            Vector3 nextPosition = player_transform.position;
            nextPosition.y = height;
            nextPosition.x += distance;
            nextPosition.z -= distance;

            transform.position = Vector3.Lerp(transform.position, nextPosition, Time.deltaTime * move_smoothness);
        }



        //transform.LookAt(player_transform.position);
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotate_smoothness * Time.deltaTime);
    }
}
