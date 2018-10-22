using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {

    // colors
    private Color[] colors = new Color[6]{
        Color.red,
        Color.blue,
        Color.green,
        Color.white,
        Color.yellow,
        Color.magenta

    };
    public SpriteRenderer sprite;

    public int index;
    public Vector2 coordinates;
    public bool destroyed;
	// Use this for initialization
	void Start () {

        index = Random.Range(0, colors.Length);
        sprite.color = colors[index];
	}
    public bool IsNeighbour(Piece otherPiece)
    {
        return Mathf.Abs(otherPiece.coordinates.x - this.coordinates.x) + Mathf.Abs(otherPiece.coordinates.y - this.coordinates.y) == 1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
