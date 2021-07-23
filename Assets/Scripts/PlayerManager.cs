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
    public GameObject finishParticles;
    public AudioClip finishSound;

    void Start()
    {
        SetOffset();
    }

    void Update()
    {
        if (!GameManager.isGameStarted || GameManager.isGameEnded)
        {
            return;
        }
        Move();
    }

    void Move()
    {
        if (IsMelted() == false)
        {
            this.transform.localScale -= Vector3.up * Time.deltaTime * scaleSpeed;   //melting
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
            //gameObject.SetActive(false);
            GameManager.instance.OnLevelFailed();
            return true;
        }
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag.Equals("Candle"))
        {
            transform.localScale += Vector3.up * candleScale;
        }
        if (other.transform.tag.Equals("Obstacle"))
        {
            var cut = Instantiate(cutPiece);
            cut.transform.position = new Vector3(this.transform.position.x, 3f, this.transform.position.z + 10f);
            //this.transform.localScale -= Vector3.up * ropeCutValue;
            this.transform.localScale -= new Vector3(0, Mathf.Clamp(ropeCutValue,0,transform.localScale.y),0);
        }
        if (other.transform.tag.Equals("Finish"))
        {
            scaleSpeed = 0;
            moveSpeed = 0;
            gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(finishSound, transform.position, 1);
            GameObject ob = Instantiate(finishParticles);
            GameManager.instance.OnLevelCompleted();
        }
        if (other.transform.tag.Equals("FinishFlag"))
        {
            Destroy(other.gameObject);
            //destroy red flag 
        }
        if (other.transform.tag.Equals("Failed"))
        {
            GameManager.instance.OnLevelFailed();
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag.Equals("Bridge"))
        {
            if (IsMelted() == false)
            {
                transform.localScale -= Vector3.up * bridgeCutValue * Time.deltaTime;
            }
        }
    }
}

