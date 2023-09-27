using UnityEngine;
using Glib.InspectorExtension;

public class PlayerJumpController : MonoBehaviour
{
    //ジャンプ力
    [SerializeField] float _jump = 5f;
    //ジャンプの回数制限
    [SerializeField] float _jumpKaisuu = 1f;
    Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField, AnimationParameter]
    private string _jumpParamName;
    Rigidbody2D _rb;
    int _count = 0;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    void Update()
    {
        //スペースを押したらジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && _count < _jumpKaisuu)
        {
            _animator.SetBool(_jumpParamName, true);
            _rb.velocity = new Vector2(0, _jump);
            _count++;
            _audioSource.Play();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _animator.SetBool(_jumpParamName, false);
            _count = 0;
        }
    }
}
