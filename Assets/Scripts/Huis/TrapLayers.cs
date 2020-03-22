using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLayers : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

	public void SetLayer(float yPosPlayer) {
		if (yPosPlayer > -2.4f) {
			spriteRenderer.sortingLayerName = "VoorPlayer";
		}
		else {
			spriteRenderer.sortingLayerName = "AchterPlayer";
		}
	}
}
