using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    SpriteRenderer sr;

    [Header("Animator")]
    [SerializeField] Animator animator;

    [Header("Statuses")]
    public bool isBounce = false;
    public bool isShield = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.material.SetFloat("_Thickness" , 0);
    }

    void Update()
    {
        Move(); 
        sr.material.SetFloat("_Thickness" ,(isShield ? .005f : 0));
        PlayerAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<CollectableObject>(out CollectableObject obj))
        {
            if(obj.cType == CandyType.None)
            {
                GameManager.Instance.Healthiness += obj.healthinessValues;
                GameManager.Instance.ObjectStore(GetHealthy(obj), collision.gameObject);
            }

            else if(obj.hType == HealthyType.None)
            {
                if(!isShield)
                    GameManager.Instance.Healthiness += obj.healthinessValues;
                else
                    isShield = false;

                GameManager.Instance.ObjectStore(GetCandy(obj), collision.gameObject);
            }

            if(isBounce)
                StartCoroutine(BounceTimer(1));
        }
    }

    private CandyType GetCandy(CollectableObject cObj)
    {
        return cObj.cType;
    }

    private HealthyType GetHealthy(CollectableObject cObj)
    {
        return cObj.hType;
    }

    #region MoveSection
    Vector3 GetDirection()
    {
        float horizontalInput = 0;

        horizontalInput = Input.GetAxis("Horizontal");

        Vector3 dirVector = new Vector3 (horizontalInput, 0, 0);

        return dirVector;
    }

    void Move()
    {
        if(!isBounce)
            rb.velocity = GetDirection() * GameManager.Instance.PlayerMoveSpeed;
    }
    #endregion

    #region BounceSection
    IEnumerator BounceTimer(int bounceTime)
    {
        yield return new WaitForSeconds(bounceTime);
        isBounce = false;
    }
    #endregion

    private void PlayerAnimation()
    {
        animator.SetBool("isFat", (GameManager.Instance.Healthiness <= GameManager.Instance.minHealthiness/2)? true: false);
        animator.SetBool("isThin", (GameManager.Instance.Healthiness >= GameManager.Instance.maxHealthiness/2)? true: false);
    }
}