using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private Rod _rod;
    private float _maxRotation;

    public Sprite[] Sprites;
    public float[] SpriteRotations;
    public Dictionary<Sprite, float> SpriteRotationMapping = new Dictionary<Sprite, float>();
    public float BallHitRotationMin = -45.0f;
    public float BallHitRotationMax = 45.0f;

    private float _rotation;
    public float Rotation
    {
        get { return _rotation; }
        set
        {
            _rotation = value;
            _spriteRenderer.sprite = GetSpriteForRotation(_rotation);
            _boxCollider2D.offset = new Vector2(_spriteRenderer.sprite.bounds.extents.x * Rotation / _maxRotation, _boxCollider2D.offset.y);
            _boxCollider2D.enabled = DisplayRotation >= BallHitRotationMin && _rotation <= BallHitRotationMax;
        }
    }

    private float DisplayRotation
    {
        get
        {
            return SpriteRotations.OrderBy(sr => Mathf.Abs(sr - Rotation)).First();
        }
    }

	private void Start()
	{
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rod = GetComponentInParent<Rod>();
        _maxRotation = SpriteRotations.Max();
        for (var i = 0; i < Sprites.Length; ++i)
        {
            SpriteRotationMapping.Add(Sprites[i], SpriteRotations[i]);
        }
    }

    private Sprite GetSpriteForRotation(float rotation)
    {
        return SpriteRotationMapping.OrderBy(sr => Mathf.Abs(sr.Value - rotation)).First().Key;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            var ball = collision.gameObject.GetComponent<Ball>();
            ball.RigidBody2D.AddForce(new Vector2(_rod.Torque, _rod.Rigidbody2D.velocity.y * Time.deltaTime), ForceMode2D.Impulse);
        }
    }
}
