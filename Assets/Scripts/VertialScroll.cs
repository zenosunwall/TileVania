using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertialScroll : MonoBehaviour
{
    [Tooltip ("Game units per second")]
    [SerializeField] float scrollRate = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = new Vector2(0f, scrollRate * Time.deltaTime);
        transform.Translate(movement);
    }
}
