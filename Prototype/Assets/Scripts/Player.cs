using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCol;
    private SpriteRenderer spR;
    private Animator anim;
    public AudioSource StepsSound;
    public Transform groundCheck, grabCheck, _transform;
    public LayerMask itIsGround;
    public float moveSpeed = 15, graundCheckRadius = 0.01f, grabCheckRadius = 0.9f, climbRadius = 1.5f, jumpSpeed = 20, Waiting = 0.9f;
    public int horizDir;
    public bool isGrouded, canGrab, climbing;
    public static bool hiden = false;
    public static int shadows = 0;
    
    void Awake()
    {
        Time.timeScale = 1;
        rb = gameObject.GetComponent<Rigidbody2D>() as Rigidbody2D;
        anim = gameObject.GetComponent<Animator>() as Animator;
        boxCol = gameObject.GetComponent<BoxCollider2D>() as BoxCollider2D;
        spR = gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
        _transform = gameObject.GetComponent<Transform>() as Transform;
        hiden = false;
        shadows = 0;
    }
    
    void FixedUpdate()
    {
        if(shadows > 0)
            hiden = true;
        else
            hiden = false;

        if(!climbing)
        {
            isGrouded = Physics2D.OverlapCircle(groundCheck.position, graundCheckRadius, itIsGround); //Проверка наличия земли под ногами
            canGrab = Physics2D.OverlapCircle(grabCheck.position, grabCheckRadius, itIsGround);

            horizDir = Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")); //Направление движения
            if(horizDir == 0)
                anim.SetBool("Move", false);
            else
                anim.SetBool("Move", true);

            if(Input.GetKey(KeyCode.Space) && isGrouded)
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y>0)
                rb.velocity = new Vector2(rb.velocity.x, 0);

            anim.SetFloat("Jump", rb.velocity.y);    

            rb.velocity = new Vector2(moveSpeed*horizDir, rb.velocity.y);
            
            if(horizDir == 1)
                spR.flipX = false;
            if(horizDir == -1)
                spR.flipX = true;
            
            if(canGrab)
                Сlimb(Physics2D.OverlapCircle(grabCheck.position, grabCheckRadius, itIsGround).gameObject); 
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 1f);
            anim.SetFloat("Jump", 0); 
        }

        if(horizDir!=0 && isGrouded)
            if(!StepsSound.isPlaying)
                StepsSound.Play();
    }

    void Сlimb(GameObject wall)
    {
        BoxCollider2D wallSize = wall.GetComponent<BoxCollider2D>(); //wall.GetComponent<SpriteRenderer>();
        Transform wallPos = wall.GetComponent<Transform>();
        //float verch = wallSize.sprite.rect.size.y/(2*wallSize.sprite.pixelsPerUnit)*wallPos.localScale.y;
        float verch = (wallSize.size.y/2+wallSize.offset.y)*wallPos.localScale.y;
        //float niz = spR.sprite.rect.size.y/(2*spR.sprite.pixelsPerUnit)*_transform.localScale.y;
        float niz = (boxCol.size.y/2-boxCol.offset.y)*_transform.localScale.y;
        if(Mathf.Abs((wallPos.position.y+verch)-_transform.position.y)<=climbRadius && Mathf.Sign(wallPos.position.x-_transform.position.x)==horizDir)
        {
            climbing = true; canGrab = false; rb.velocity = new Vector2(rb.velocity.x, 1f);
            StartCoroutine(InProgress(Waiting, new Vector3(_transform.position.x+horizDir,wallPos.position.y+verch+niz,0))); 
            _transform.position = Vector3.Lerp(_transform.position, new Vector3(_transform.position.x, wallPos.position.y+verch, 0), 1);
            anim.SetBool("Climbing", true);
        }
    }

    IEnumerator InProgress(float timer, Vector3 smesh)
    {
        yield return new WaitForSeconds(timer);
        climbing = false;
        anim.SetBool("Climbing", false);
        _transform.position = Vector3.Lerp(_transform.position, smesh, 1);
        rb.velocity = new Vector2(rb.velocity.x, 1f);
    }
}
