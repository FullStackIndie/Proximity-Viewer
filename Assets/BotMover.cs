using UnityEngine;

public class BotMover : MonoBehaviour
{
    [SerializeField] int speed = 10;
    [SerializeField] GameObject hider;
    [SerializeField] GameObject hider1;


// OnTrigger only works if the gameobject this script is attached to has a Rigidbody2D
// and if the gameobject your touching, in this case it is hider and hider2, has a 2D collider (example Box Collider2D, Sphere Collider 2D)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == hider) // if collided gamobject equals the hider gameobject
        {
            Debug.Log($"{hider} has been found, gonna destroy");
            Destroy(hider, 2f);// destroys hider gameobject after 2 seconds(f stands for float)
        }

        if (collision.gameObject == hider1)
        {
            Debug.Log($"{hider1} has been found, gonna destroy");
            Destroy(hider1, 2f); // destroys hider1 gameobject after 2 seconds(f stands for float)
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D)) // if you press D it transform goes right
        {
            transform.position = new Vector2(transform.position.x + 1f * Time.deltaTime * speed, transform.position.y) ;
        }  

        if (Input.GetKey(KeyCode.A))// if you press A it transform goes left
        {
            transform.position = new Vector2(transform.position.x - 1f * Time.deltaTime * speed, transform.position.y);
        }

        if (Input.GetKey(KeyCode.W))// if you press W it transform goes up
        {
            transform.position = new Vector2(transform.position.x , transform.position.y + 1f * Time.deltaTime * speed);
        }

        if (Input.GetKey(KeyCode.S))// if you press S it transform goes down
        {
            transform.position = new Vector2(transform.position.x , transform.position.y - 1f * Time.deltaTime * speed);
        }

        if (Input.GetKeyDown(KeyCode.Space))// if you press space it transform goes up a little bit every time you press the space bar
        {
            transform.position = new Vector2(transform.position.x , transform.position.y + 1f * Time.deltaTime * speed * 2);
        }
    }
}
