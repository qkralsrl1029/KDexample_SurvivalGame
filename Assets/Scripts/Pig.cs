using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pig : MonoBehaviour
{

    [SerializeField] private string animalName; // 동물의 이름
    [SerializeField] private int hp; // 동물의 체력.

    [SerializeField] private float walkSpeed; // 걷기 스피드.
    [SerializeField] private float runSpeed; // 뛰기 스피드.
    private Vector3 destination; // nav dest


    // 상태변수
    private bool isAction; // 행동중인지 아닌지 판별.
    private bool isWalking; // 걷는지 안 걷는지 판별.
    private bool isRunning; // 뛰는지 판별.
    private bool isDead; // 죽었는지 판별.

    [SerializeField] private float walkTime; // 걷기 시간
    [SerializeField] private float waitTime; // 대기 시간.
    [SerializeField] private float runTime; // 뛰기 시간.
    private float currentTime;


    // 필요한 컴포넌트
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private BoxCollider boxCol;
    private AudioSource theAudio;
    private NavMeshAgent nav;

    [SerializeField] private AudioClip[] sound_pig_Normal;
    [SerializeField] private AudioClip sound_pig_Hurt;
    [SerializeField] private AudioClip sound_pig_Dead;

    // Use this for initialization
    void Start()
    {
        theAudio = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Move();
            ElapseTime();
        }
    }

    private void Move()
    {
        if (isWalking || isRunning)
            nav.SetDestination(transform.position+ destination*3f);
            //nav agent가 rigidbody 강제로 잠금
            //rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
    }


    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                ReSet();
        }
    }

    private void ReSet()
    {
        isWalking = false; isRunning = false; isAction = true;
        nav.speed = walkSpeed;
        nav.ResetPath();
        anim.SetBool("Walk", isWalking); anim.SetBool("Run", isRunning);
        destination.Set(Random.Range(-0.2f,0.2f),0f,Random.Range(0.5f,1f));
        RandomAction();
    }

    private void RandomAction()
    {
        RandomSound();

        int _random = Random.Range(0, 4); // 대기, 풀뜯기, 두리번, 걷기.

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Eat();
        else if (_random == 2)
            Peek();
        else if (_random == 3)
            TryWalk();
    }

    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }
    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("풀뜯기");
    }
    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
        Debug.Log("두리번");
    }
    private void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walk", isWalking);
        currentTime = walkTime;
        nav.speed = walkSpeed;
        Debug.Log("걷기");
    }

    private void Run(Vector3 _targetPos)
    {
        destination = new Vector3(transform.position.x - _targetPos.x, 0f, transform.position.z - _targetPos.z).normalized;
        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        nav.speed = runSpeed;
        anim.SetBool("Run", isRunning);
    }

    public void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _dmg;

            if (hp <= 0)
            {
                Dead();
                return;
            }

            PlaySE(sound_pig_Hurt);
            anim.SetTrigger("Hurt");
            Run(_targetPos);
        }
    }

    private void Dead()
    {
        PlaySE(sound_pig_Dead);
        isWalking = false;
        isRunning = false;
        isDead = true;
        nav.ResetPath();
        anim.SetTrigger("Dead");
    }

    private void RandomSound()
    {
        int _random = Random.Range(0, 3); // 일상 사운드 3개.
        PlaySE(sound_pig_Normal[_random]);
    }

    private void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }
}
