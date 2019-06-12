using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neremnem.Tools;
///<summary>
/// 적, 몬스터 클래스
///</summary>

[DisallowMultipleComponent]
public class Enemy : Unit
{
    private enum EPhase
    {
        Non
      , One
      , Two
      , Three
      , Four
      , Die
    }
    /// <summary>
    /// 각 페이즈 당 몇 개의 패턴을 활성화 시킬 것인지
    /// </summary>
    [Header("Phase Setting")]
    public int[] PhaseOne;
    public int[] PhaseTwo;
    public int[] PhaseThree;
    public int[] PhaseFour;
    [Header("Phase HealthPoint Setting")]
    /// <summary>
    /// 각 페이즈 당 최소 HP
    /// </summary>
    public int PhaseOneMin = 80;
    public int PhaseTwoMin = 40;
    public int PhaseThreeMin = 10;
    public int PhaseFourMin = 0;
    [Header("Gold Setting")]
    public int MaxGold = 0;
    public int MinGold = 0;

    [HideInInspector]
    public bool bDie = false;

    /// <summary>
    /// 초기 과거, 현재 페이즈 상태
    /// </summary>
    private EPhase mPresentPhase = EPhase.Non;
    private EPhase mPastPhase = EPhase.Non;
    private Vector3 mStartPos;
    /// <summary>
    /// 어빌리티 리스트
    /// </summary>
    private List<EnemyAbility> mAbilities;
    private Component[] AbilityComponent;
    private EnemyAbility mEnemyAbillity= null;


    private void Awake()
    {
        mAbilities = new List<EnemyAbility>();    
    }
    private void OnEnable()
    {
        bDie = false;
        GetComponent<Rigidbody>().isKinematic = false;
        if (mEnemyAbillity == null)
        {
            mEnemyAbillity = GetComponent<EnemyAbility>();
        }
        if (mEnemyAbillity.anim != null && mEnemyAbillity.GetAnimBool("isAttack"))
        {
            mEnemyAbillity.SetAnimBool("isDie", false);
        }
        Active();
        mPresentPhase = EPhase.Non;
        mPastPhase = EPhase.Non;
        EventManager.StartListeningTakeDamageEvent("PlayersAttack", TakeDamage);   
    }
    private void OnDisable()
    {
        StopCoroutine("ActiveFalseDelay");
        EventManager.StopListeningTakeDamageEvent("PlayersAttack", TakeDamage);
    }
    // Start is called before the first frame update
    void Start()
    {
        mbIsActive = true;
        this.gameObject.tag = "Enemy";
        mStartPos = this.transform.position;
        AbilityComponent = GetComponentsInParent(typeof(EnemyAbility));

        if (AbilityComponent != null)
        {
            foreach (EnemyAbility ability in AbilityComponent)
            {
                RegisterAbility(ability);
            }
        }
    }

    void Update()
    {
        if (hp > PhaseOneMin)
        {
            mPresentPhase = EPhase.One;
        }
        else if (hp <= PhaseOneMin && hp > PhaseTwoMin)
        {
            mPresentPhase = EPhase.Two;
        }
        else if (hp <= PhaseTwoMin && hp > PhaseThreeMin)
        {
            mPresentPhase = EPhase.Three;
        }
        else if (hp <= PhaseThreeMin && hp > PhaseFourMin)
        {
            mPresentPhase = EPhase.Four;
        }
        else
        {
            mPresentPhase = EPhase.Die;
        }

        if (mPresentPhase != mPastPhase)
        {
            Debug.Log("Change");
            PatternManager(mPresentPhase);
        }
    }

    /// <summary>
    /// 대미지 이벤트
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="i"></param>
    private void TakeDamage(GameObject gameObject, int i)
    {
        if(gameObject == this.gameObject)
        {
            hp -= i;
            Debug.Log("monster: " + hp);
        }
    }

    /// <summary>
    /// 패턴 실행 함수
    /// </summary>
    /// <param name="phaseNum"></param>
    private void PatternManager(EPhase phaseNum)
    {
        mPastPhase = phaseNum;
        switch (phaseNum)
        {
            case EPhase.One:
                DisableAbilities();
                for (int i = 0; i < PhaseOne.Length; i++)
                    mAbilities[PhaseOne[i]].enabled = true;
                break;
            case EPhase.Two:
                DisableAbilities();
                for (int i = 0; i < PhaseTwo.Length; i++)
                    mAbilities[PhaseTwo[i]].enabled = true;
                break;
            case EPhase.Three:
                DisableAbilities();
                for (int i = 0; i < PhaseThree.Length; i++)
                    mAbilities[PhaseThree[i]].enabled = true;
                break;
            case EPhase.Four:
                DisableAbilities();
                for (int i = 0; i < PhaseFour.Length; i++)
                    mAbilities[PhaseFour[i]].enabled = true;
                break;
            case EPhase.Die:
                Die();
                break;
            default:
                Debug.Log("Health Point Range Error");
                break;
        }

    }

    /// <summary>
    /// 죽었을 때 초기화 하는 함수
    /// </summary>
    protected override void Die() 
    {
        bDie = true;
        mEnemyAbillity.SetAnimBool("isDie",true);
        DisableAbilities();
        
        InActive();
        GetComponent<Rigidbody>().isKinematic = true;

        StartCoroutine("ActiveFalseDelay");
    }

    /// <summary>
    /// 활성화 하는 함수 Max Hp, Spawn Point etx
    /// </summary>
    protected override void Active()
    {
        base.Active();
        InitHP();
        GetComponent<Rigidbody>().isKinematic = false;
    }

    /// <summary>
    /// 모든 어빌리티 비활성화 함수
    /// </summary>
    void DisableAbilities()
    {
        for (int i = 0; i < mAbilities.Count; i++)
        {
            mAbilities[i].StopAllCoroutines();
            mAbilities[i].enabled = false;
        }
    }

    /// <summary>
    /// 코인 스폰
    /// </summary>
    /// <param name="pos"></param>
    void SpawnCoin(Vector3 pos)
    {
        List<GameObject> mCoins = GameObject.Find("ItemManager").GetComponent<ObjectPooling>().obejcts;
        //코인량 랜덤
        //int coinCount = Random.Range(1, mCoins.Count);
        //for (int i = 0; i < coinCount; i++)
        //{
        //    if (mCoins[i].activeSelf == false)
        //    {
        //        mCoins[i].transform.position = pos;
        //        mCoins[i].SetActive(true);
        //    }
        //}
        for (int i = 0; i < MaxGold; i++)
        {
            if (mCoins[i].activeSelf == false)
            {
                mCoins[i].transform.position = pos;
                mCoins[i].SetActive(true);
            }
        }
    }

    /// <summary>
    /// 아이템 스폰
    /// </summary>
    /// <param name="pos"></param>
    void SpawnItem(Vector3 pos)
    {
        List<GameObject> items = GameObject.Find("ItemManager").GetComponent<ItemObjectPooling>().ItemObjects;
        int itemSelect = Random.Range(0,items.Count);
        if(items[itemSelect].activeSelf == false)
        {
            items[itemSelect].transform.position = pos;
            items[itemSelect].SetActive(true);
        }
    }
    
    /// <summary>
    /// mAbilitise에 어빌리티 상속받는 스크립트 등록
    /// </summary>
    /// <param name="ability"></param>
    public void RegisterAbility(EnemyAbility ability)
    {
        if ((ability != GetComponent<EnemyAbility>())
            && (ability != GetComponent<EnemyMovement>()))
        {
            Debug.Log(ability);
            mAbilities.Add(ability);
        }
    }

    //몇 초 뒤에 사라질 것인지
    IEnumerator ActiveFalseDelay()
    {
        yield return new WaitForSeconds(activeFalseTime);
        SpawnCoin(gameObject.transform.position);
        SpawnItem(gameObject.transform.position);
        gameObject.SetActive(false);
    }

}
