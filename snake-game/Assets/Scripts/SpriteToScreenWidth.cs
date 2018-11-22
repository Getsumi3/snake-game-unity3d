using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteToScreenWidth : MonoBehaviour {

    public bool constHeight;
    public bool constWidth;

	void Awake () {
        ResizeSpriteToScreen();
	}

    private void ResizeSpriteToScreen()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        //transform.localScale = new Vector3(1, 1, 1);

        double width = sr.sprite.bounds.size.x;
        double height = sr.sprite.bounds.size.y;

        double worldScreenHeight = Camera.main.orthographicSize * 2.0;
        double worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        double x = width/worldScreenWidth; // width;
        double y = height/worldScreenHeight; // height;

        if (constHeight) { transform.localScale = new Vector2((float)x/2, transform.localScale.y); return; }
        if (constWidth) { transform.localScale = new Vector2(transform.localScale.x, (float)y/2); return; }
    }

}
