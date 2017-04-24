using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodController : MonoBehaviour
{
    private Rod _rod; 

    public bool IsControlled;
    public float MovementSpeed = 2.0f;

	private void Start()
	{
        _rod = GetComponent<Rod>();
        StartCoroutine(ControllerLoop());
    }

    private IEnumerator ControllerLoop()
    {
        while (true)
        {
            if (Input.GetMouseButton(0))
            {
                var startMousePosition = Input.mousePosition;
                yield return new WaitForEndOfFrame();
                var mousePositionDelta = Input.mousePosition - startMousePosition;
                _rod.Rotation += mousePositionDelta.x;
                _rod.Rigidbody2D.AddForce(new Vector2(0, mousePositionDelta.y) * MovementSpeed);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
