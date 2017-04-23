using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreIndicator : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public Sprite[] ScoreSprites = { };

    private int _score = 0;
    public int Score
    {
        get { return _score; }
        set
        {
            _score = value % ScoreSprites.Length;
            _spriteRenderer.sprite = ScoreSprites[_score];
        }
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
		
	}
}
