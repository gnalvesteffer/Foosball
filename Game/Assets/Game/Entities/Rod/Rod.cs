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
            _rotation = value;
            foreach(var guy in _guys)
            {
                guy.Rotation = _rotation;
            }
        }
    }

	private void Start()
	{
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _guys = GetComponentsInChildren<Guy>();
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
