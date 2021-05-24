using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MoveBox : MonoBehaviour
{
    

    [SerializeField]
    private Vector3 setDir = new Vector3(0,0,0);
    [SerializeField]
    private Material active = null;
    [SerializeField]
    private Material inactive = null;
    
    private MeshRenderer meshRenderer = null;
    float segSize = 0;
    int bSize = 0;
    float offset = 0;
    float halfBoard = 0;

    Vector3 snDir = new Vector3(0, 0, 0);
    
    void Awake()
    {
        GetComponent<XRSimpleInteractable>().onHoverEnter.AddListener(SetDir);
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        segSize = GameManager.boxSize;
        bSize = GameManager.boardSize;
        offset = -segSize / 2;
        halfBoard = segSize * bSize / 2;

        var scale = transform.localScale;
        scale.x = (segSize * bSize) - ((segSize * bSize - 0.001f) * setDir.x * setDir.x);
        scale.y = (segSize * bSize) - ((segSize * bSize - 0.001f) * setDir.y * setDir.y);
        scale.z = (segSize * bSize) - ((segSize * bSize - 0.001f) * setDir.z * setDir.z);
        transform.localScale = scale;

        transform.localPosition = new Vector3(offset + (halfBoard * setDir.x), offset + (halfBoard * setDir.y), offset + (halfBoard * setDir.z));

    }

    void Update()
    {
        snDir = GameBoard.snakeDir;

        if (snDir.x == setDir.x && snDir.y == setDir.y && snDir.z == setDir.z)
            meshRenderer.material = active;
        else
            meshRenderer.material = inactive;
        
        transform.localPosition = new Vector3(offset + (halfBoard * setDir.x), offset + (halfBoard * setDir.y), offset + (halfBoard * setDir.z));
    }

    private void SetDir(XRBaseInteractor interactor)
    {

        GameBoard.snakeDir = setDir;
        //GameBoard.move = true;
    }
}
