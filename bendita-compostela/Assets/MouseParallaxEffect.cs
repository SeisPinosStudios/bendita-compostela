using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseParallaxEffect : MonoBehaviour
{
    [SerializeField] private GameObject[] layers;
    [SerializeField] private float MouseSpeedX = 0.05f;
    private Vector3[] OriginalPositions;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        OriginalPositions = new Vector3[layers.Length];

        for(int i = 0; i<layers.Length; i++)
        {
            OriginalPositions[i] = layers[i].transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x, y;
        x = (Input.mousePosition.x - (Screen.width / 2)) * MouseSpeedX / Screen.width;
 

        for(int i = 1; i <layers.Length+1; i++)
        {
            layers[i - 1].transform.position = OriginalPositions[i - 1] + (new Vector3(x, 0f,  0f) * i * ((i - 1) - (layers.Length / 2)));
        }

    }
}
