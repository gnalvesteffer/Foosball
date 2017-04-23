using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

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
        for (var i = 0; i < Sprites.Length; ++i)
        {
            SpriteRotationMapping.Add(Sprites[i], SpriteRotations[i]);
        }
    }
	
	private void Update()
	{
		
	}

    private Sprite GetSpriteForRotation(float rotation)
    {
        return SpriteRotationMapping.OrderBy(sr => Mathf.Abs(sr.Value - rotation)).First().Key;
    }
}
