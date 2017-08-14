using UnityEngine;

public class Player : MonoBehaviour {

    public Transform targent;
    private Rigidbody2D playerRig;
    private int bounce = 500;
    private System.Random random;
    //private Camera cam;
    //private Vector3 screenPos;

	// Use this for initialization
	void Start () {
        random = new System.Random();
        playerRig = GetComponent<Rigidbody2D>();
        //cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump"))
        {
            playerRig.AddForce(new Vector2(0, bounce));
            //screenPos = cam.WorldToScreenPoint(targent.position);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {   
            float bounceX = random.Next(-bounce, bounce);
            float bounceY = random.Next(-bounce, bounce);
            playerRig.AddForce(new Vector2(bounceX,bounceY));
            if(bounce>0) bounce -= 50;
        }
        if(collision.gameObject.tag == "Player")
        {
            float bounceX = random.Next(-bounce, bounce);
            float bounceY = random.Next(-bounce, bounce);
            playerRig.AddForce(new Vector2(bounceX,bounceY));
            if (bounce > 0) bounce -= 50;
        }
        if (collision.gameObject.tag == "Wall")
        {
            float bounceX = random.Next(-bounce, bounce);
            float bounceY = random.Next(-bounce, bounce);
            playerRig.AddForce(new Vector2(bounceX, bounceY));
            if (bounce > 0) bounce -= 50;
        }
    }
}
