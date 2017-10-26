using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]

public class Controller2D : MonoBehaviour
{
    public LayerMask collisionMask;

    const float skinWidth = .015f;

    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    float horizontalRaySpacing;
    float verticalRaySpacing;

    [HideInInspector]
    public BoxCollider2D collider;
    RaycastOrigins raycastOrigins;

    public CollisionInfo collisions;

	//Invisibility
	bool canUse;
	public float invisibleTimer = 5f;
	public float invisibilityCooldown = 40f;

	[HideInInspector]
	public bool isInvisible = false;

    //Attack Bogus implementation
    public GameObject attackLeft, attackRight;
    public float attackCoolDown = 1;
    //Direction
    SpriteRenderer sprite;

	public static Controller2D Instance { get { return _instance; } } //getter for instance
	static protected Controller2D _instance; //declaring instance variable

	void Awake()
	{
		if (_instance != null)
		{
			Debug.LogWarning("There is alreade a Controller2D in play. Deleting old, instantiating new");
			Destroy(Controller2D.Instance.gameObject);
			_instance = null;
		}
		else
		{
			_instance = this;
		}
	}

    void Start()
    {
		attackLeft.SetActive(false);
		attackRight.SetActive(false); 
        sprite = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();

		canUse = false;
    }

    public void Move(Vector3 velocity)
    {
        collisions.Reset();
        UpdateRaycastOrigins();

        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }
        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }

        transform.Translate(velocity);
    }

    //Changing direction Works, However I need to inverse the inputs or flip the sprite texture
    public void Direction(Vector2 direction) //input
    {
        if (direction.x > 0) //going right
        {
            sprite.flipX = false;
        }

        if (direction.x < 0) //going left
        {
            sprite.flipX = true;
        }
    }

    IEnumerator hideAttackRadius(float time)
    {
        yield return new WaitForSeconds(time);
		attackRight.SetActive(false);
		attackLeft.SetActive (false);
    }

    //attack in the correct direction
    public void Attack()
    {
		if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            if (!sprite.flipX) //facing right
            {
                Debug.Log("Attacking Right");
				attackRight.SetActive(true);
                StartCoroutine(hideAttackRadius(attackCoolDown));
            }
            else 
            {
                Debug.Log("Attacking Left");
				attackLeft.SetActive(true);
                StartCoroutine(hideAttackRadius(attackCoolDown));
            }
        }
    }

	IEnumerator powerUpCoolDown()
	{
		yield return new WaitForSeconds (invisibleTimer);
		sprite.enabled = true;
        //cue sound
        AudioEventSystem.PlayerInvisibleOff();
		isInvisible = false;
		yield return new WaitForSeconds (invisibilityCooldown);
		canUse = true;
	}

	public void activatePowerUp()
	{
		canUse = true;
	}

    public void Invisibility()
    {
		if (Input.GetKeyDown (KeyCode.I) && canUse) {
			Debug.Log ("Invisible");

			if (canUse) {

                //cue the sound
                AudioEventSystem.PlayerInvisibleOn();
				//turn sprite off 
				sprite.enabled = false;
				//For enemy script
				isInvisible = true;
				
				canUse = false;
				//run cooldown timer  
				StartCoroutine (powerUpCoolDown ());
			} 
		}
    }


    void HorizontalCollisions(ref Vector3 velocity)
    {

        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {

            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, collisionMask);
            Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;
            }
        }
    }

    void VerticalCollisions(ref Vector3 velocity)
    {

        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, collisionMask);

            Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);

            if (hit)
            {

                velocity.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                collisions.below = directionY == -1;
                collisions.above = directionY == 1;
            }
        }
    }

    void UpdateRaycastOrigins()
    {

        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing()
    {

        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }

}