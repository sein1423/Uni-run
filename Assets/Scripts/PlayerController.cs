using UnityEngine;

// PlayerController는 플레이어 캐릭터로서 Player 게임 오브젝트를 제어한다.
public class PlayerController : MonoBehaviour {
   public AudioClip deathClip; // 사망시 재생할 오디오 클립
   public float jumpForce = 700f; // 점프 힘

   private int jumpCount = 0; // 누적 점프 횟수
   private bool isGrounded = false; // 바닥에 닿았는지 나타냄
   private bool isDead = false; // 사망 상태
   public bool isGetStar = false;

   private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
   private Animator animator; // 사용할 애니메이터 컴포넌트
   private AudioSource playerAudio; // 사용할 오디오 소스 컴포넌트

   private void Start() {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

   private void Update() {
        if (isDead) return;

        if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(Vector2.up * jumpForce);
            playerAudio.Play();
        }
        else if(Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0f)
        {
            playerRigidbody.velocity *= .5f;
        }
        animator.SetBool("Grounded", isGrounded);
   }

   private void Die() {
        if (!isGetStar)
        {

            animator.SetTrigger("Die");
            playerAudio.clip = deathClip;
            playerAudio.Play();

            playerRigidbody.velocity = Vector2.zero;
            isDead = true;

            GameManager.instance.OnPlayerDead();

            playerRigidbody.isKinematic = true;
        }

   }

   private void OnTriggerEnter2D(Collider2D other) {
        if (!isDead)
        {
            if (other.tag == "Dead" && !isGetStar)
            {
                Die();
            }
            else if (other.tag == "Item")
            {
                Destroy(other.gameObject);
                isGetStar = true;
            }
            else
            {
                Destroy(other.gameObject);
                jumpCount--;
            }
        }
   }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Dead") && isGetStar)
        {
            isGetStar = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
       if(collision.contacts[0].normal.y > .7f)//고양이할때 천장에 머리박으면 점프뛰는거 해소해주는 코드
        {
            isGrounded = true;
            jumpCount = 0;
        }
   }

   private void OnCollisionExit2D(Collision2D collision) {
        isGrounded = false;
   }
}