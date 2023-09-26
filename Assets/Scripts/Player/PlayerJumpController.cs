using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    //�W�����v��
    [SerializeField] float _jump = 5f;
    //�W�����v�̉񐔐���
    [SerializeField] float _jumpKaisuu = 1f;
    Rigidbody2D _rb;
    int _count = 0;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        //�X�y�[�X����������W�����v
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
