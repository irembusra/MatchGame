using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour {

    public int boardWidth = 6;
    public int boardHeight = 5;
    public float pieceSpacing = 1.4f;

    public Camera gameCamera;
    public Transform levelContainer;

    public GameObject piecePrefab;

    private int score;
    private bool gameOver;

    private Piece[,] board;
    private Piece selectedPiece;
	// Use this for initialization
	void Start () {
        BuildBoard();
	}
    private void BuildBoard()
    {
        board = new Piece[boardWidth, boardHeight];
        for(int y=0; y< boardHeight; y++)
        {
            for(int x=0; x<boardWidth; x++)
            {
                GameObject pieceObject = Instantiate(piecePrefab);
                pieceObject.transform.SetParent(levelContainer);
                pieceObject.transform.localPosition=new Vector3(
                    (-boardWidth*pieceSpacing)/2f+(pieceSpacing/2f)+x*pieceSpacing ,//x
                    (-boardHeight*pieceSpacing)/2f+(pieceSpacing/2f) + y* pieceSpacing,//y
                    0);//z

                Piece piece = pieceObject.GetComponent<Piece>();
                piece.coordinates = new Vector2(x, y);
                board[x, y] = piece;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        ProcessInput();
		
	}
    private void ProcessInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = gameCamera.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            if(hitCollider != null&& hitCollider.gameObject.GetComponent<Piece>() != null)
            {
                Piece hitPiece = hitCollider.gameObject.GetComponent<Piece>();
                if (selectedPiece == null)
                {
                    selectedPiece = hitPiece;

                    iTween.ScaleTo(selectedPiece.gameObject, iTween.Hash(
                        "scale", Vector3.one * 1.5f,
                        "time", 0.2f
                        ));
                }
                else
                {
                    if(hitPiece==selectedPiece || hitPiece.IsNeighbour(selectedPiece) == false)
                    {
                        iTween.ScaleTo(selectedPiece.gameObject, iTween.Hash(
                            "scale",Vector3.one*2.0f,
                            "time",0.2f));
                    }else if (hitPiece.IsNeighbour(selectedPiece))
                    {
                        //AttemptMatch(selectedPiece, hitPiece);
                    }
                    selectedPiece = null;
                }
            }
        }
    }
}
