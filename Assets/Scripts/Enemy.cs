using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D phyiscs;
    SpriteRenderer enemySprite;

    private float xDirection;

    // Start is called before the first frame update
    void Start()
    {
        xDirection = 1f;
        phyiscs = GetComponent<Rigidbody2D>();
        enemySprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        phyiscs.velocity = new Vector2(moveSpeed * xDirection, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipDirection();
    }

    private void FlipDirection()
    {
        xDirection *= -1f;
        transform.localScale = new Vector2(xDirection, 1f);
    }
}
