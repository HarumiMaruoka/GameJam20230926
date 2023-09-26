using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    //ジャンプ力
    [SerializeField] float _jump = 5f;
    //ジャンプの回数制限
    [SerializeField] float _jumpKaisuu = 1f;
    Rigidbody2D _rb;
    int _count = 0;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //スペースを押したらジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && _count < _jumpKaisuu)
        {
            _rb.velocity = new Vector2(0, _jump);
            _count++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _count = 0;
        }
    }
}
