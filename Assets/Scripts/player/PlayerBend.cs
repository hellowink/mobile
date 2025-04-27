using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerBend : MonoBehaviour
{
    public SpriteRenderer defaultSpriteRenderer;
    public SpriteRenderer bendSpriteRenderer;
    public BoxCollider2D playerCollider;

    private Vector2 originalColliderSize;
    private Vector2 originalColliderOffset;
    private bool isBending = false;

    void Start()
    {
        if (playerCollider != null)
        {
            originalColliderSize = playerCollider.size;
            originalColliderOffset = playerCollider.offset;
        }

        bendSpriteRenderer.enabled = false;
    }

    void Update()
    {
        if (isBending)
        {
            BendPlayer();
        }
        else
        {
            ResetPlayer();
        }
    }

    // Estas funciones nuevas SIN parámetros
    public void StartBending()
    {
        isBending = true;
    }

    public void StopBending()
    {
        isBending = false;
    }

    void BendPlayer()
    {
        defaultSpriteRenderer.enabled = false;
        bendSpriteRenderer.enabled = true;

        if (playerCollider != null)
        {
            playerCollider.size = new Vector2(originalColliderSize.x, originalColliderSize.y / 2f);
            playerCollider.offset = new Vector2(originalColliderOffset.x, originalColliderOffset.y - (originalColliderSize.y / 4f));
        }
    }

    void ResetPlayer()
    {
        defaultSpriteRenderer.enabled = true;
        bendSpriteRenderer.enabled = false;

        if (playerCollider != null)
        {
            playerCollider.size = originalColliderSize;
            playerCollider.offset = originalColliderOffset;
        }
    }
}