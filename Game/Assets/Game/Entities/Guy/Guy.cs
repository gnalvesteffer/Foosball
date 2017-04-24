using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Rod _rod;

    public Sprite[] Sprites;
    public float[] SpriteRotations;
    public Dictionary<Sprite, float> SpriteRotationMapping = new Dictionary<Sprite, float>();

    private float _rotation;
    public float Rotation
    {
        get { return _rotation; }
        set
        {
            _rotation = value;
            _spriteRenderer.sprite = GetSpriteForRotation(_rotation);
        }
    }

	private void Start()
	{
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rod = GetComponentInParent<Rod>();
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
