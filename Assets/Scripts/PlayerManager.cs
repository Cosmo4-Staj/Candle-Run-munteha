using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] float scaleSpeed = .5f;
    [SerializeField] float minSize = 0.0f;
    [SerializeField] float candleScale = 0f;
    [SerializeField] float moveSpeed = .5f;
    [SerializeField] float ropeCutValue = 1f;
    [SerializeField] float bridgeCutValue = 4f;

    Vector3 curOffset, mouseOffset;
    GameObject playerOffsetX;
    public Vector2 rangePlayerPosX;
    public GameObject cutPiece;

    void Start()
    {
        SetOffset();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (IsMelted() == false)
        {
            this.transform.localScale -= Vector3.up * Time.deltaTime * scaleSpeed;   //melting
        }

        this.transform.position += this.transform.forward * Time.deltaTime * moveSpeed;

        float pos = (Input.mousePosition.x * (rangePlayerPosX.y - rangePlayerPosX.x)) / Screen.width;
        pos -= (rangePlayerPosX.y - rangePlayerPosX.x) / 2;

        //for the right left movement according to the place we have clicked 
        if (Input.GetMouseButtonDown(0))
        {
            playerOffsetX.transform.position = new Vector3(pos, this.transform.position.y, this.transform.position.z);
            mouseOffset = this.transform.position - playerOffsetX.transform.position;
        }
        //still holding the click
        else if (Input.GetMouseButton(0))
        {
            playerOffsetX.transform.position = new Vector3(pos, this.transform.position.y, this.transform.position.z);
            mouseOffset.y = mouseOffset.z = 0;
            curOffset = playerOffsetX.transform.position + mouseOffset;
            curOffset.x = Mathf.Clamp(curOffset.x, rangePlayerPosX.x, rangePlayerPosX.y);
            this.transform.position = curOffset;
        }
    }

    //creating game object to keep distance between clicked position and the object
    void SetOffset()
    {
        playerOffsetX = new GameObject();
        playerOffsetX.name = "GameObjectOffset";
        playerOffsetX.transform.position = this.transform.position;
    }

    public bool IsMelted()
    {
        if (this.transform.localScale.y <= minSize)
        {
            moveSpeed = 0;
            return true;
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Candle"))
        {
            transform.localScale += Vector3.up * candleScale;
            Destroy(other.gameObject);
        }
        if (other.transform.tag.Equals("Obstacle"))
        {
            var cut = Instantiate(cutPiece);
            cut.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 10f);
            this.transform.localScale -= Vector3.up * ropeCutValue ;
        }
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag.Equals("Bridge"))
        {
            if(IsMelted() == false)
            {
                transform.localScale -= Vector3.up * bridgeCutValue * Time.deltaTime;
            }
        }
    }
}

