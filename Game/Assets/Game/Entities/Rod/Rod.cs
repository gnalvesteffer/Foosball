using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rod : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Guy[] _guys;

    public Sprite[] Sprites;
    public float[] SpriteRotations;
    public Dictionary<Sprite, float> SpriteRotationMapping = new Dictionary<Sprite, float>();

    private float _rotation;
    public float Rotation
    {
        get { return _rotation; }
        set
        {
            _rotation = Mathf.Clamp(value, -90, 90);
            _spriteRenderer.sprite = GetSpriteForRotation(_rotation);
            foreach (var guy in _guys)
            {
                guy.Rotation = _rotation;
            }
        }
    }

    private Rigidbody2D _rigidbody2D;
    public Rigidbody2D Rigidbody2D
    {
        get { return _rigidbody2D; }
        set { _rigidbody2D = value; }
    }

    public float Torque { get; private set; }

	private void Start()
	{
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _guys = GetComponentsInChildren<Guy>();
        for (var i = 0; i < Sprites.Length; ++i)
        {
            SpriteRotationMapping.Add(Sprites[i], SpriteRotations[i]);
        }
        StartCoroutine(ProcessTorque());
    }

    private IEnumerator ProcessTorque()
    {
        while(true)
        {
            var startRotation = Rotation;
            yield return new WaitForSeconds(0.01f);
            Torque = (Rotation - startRotation) * 0.01f;
        }
    }

    private Sprite GetSpriteForRotation(float rotation)
    {
        return SpriteRotationMapping.OrderBy(sr => Mathf.Abs(sr.Value - rotation)).First().Key;
    }
}
