using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{

    private float H;
    private float V;
    public float speed;
    public GameObject particle;
    public float zoomspeed;
    public Vector3 startposition;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = startposition;
    }

    // Update is called once per frame
    void Update()
    {
        H = Input.GetAxis("Horizontal");
        V = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * H * speed * Time.deltaTime * (Camera.main.orthographicSize / 2));
        transform.Translate(Vector3.up * V * speed * Time.deltaTime * (Camera.main.orthographicSize / 2));

        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(particle, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        }
        if (Input.GetKey("o"))
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + (zoomspeed + (Camera.main.orthographicSize / 128));
        }
        if (Input.GetKey("i"))
        {
            if (Camera.main.orthographicSize > 10)
            {
                Camera.main.orthographicSize = Camera.main.orthographicSize - (zoomspeed + (Camera.main.orthographicSize / 128));
            }
        }
        if (Input.GetKey("r"))
        {
            Camera.main.orthographicSize = 16;
            transform.position = startposition;
        }
    }
}
