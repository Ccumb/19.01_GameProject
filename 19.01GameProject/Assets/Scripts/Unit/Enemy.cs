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
    public int[] PhaseOne;
    public int[] PhaseTwo;
    public int[] PhaseThree;
    public int[] PhaseFour;
    [HideInInspector]
    public bool bDie = false;

    private EPhase mPresentPhase = EPhase.Non;
    private EPhase mPastPhase = EPhase.Non;
    private Vector3 mStartPos;
    private List<EnemyAbility> mAbilities;
    private Component[] AbilityComponent;


    private void Awake()
    {
        mAbilities = new List<EnemyAbility>();    
    }
    private void OnEnable()
    {
        bDie = false;
        InitHP();
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
        if (hp > 80)
        {
            mPresentPhase = EPhase.One;
        }
        else if (hp <= 80 && hp > 40)
        {
            mPresentPhase = EPhase.Two;
        }
        else if (hp <= 40 && hp > 10)
        {
            mPresentPhase = EPhase.Three;
        }
        else if (hp <= 10 && hp > 0)
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
    private void TakeDamage(GameObject gameObject, int i)
    {
        if(gameObject == this.gameObject)
        {
            hp -= i;
            Debug.Log("monster: " + hp);
        }
    }

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

    protected override void Die() 
    {
        bDie = true;
        DisableAbilities();
        SpawnCoin(this.transform.position);
        
        InActive();
        GetComponent<Rigidbody>().isKinematic = true;

        StartCoroutine("ActiveFalseDelay");
    }

    protected override void Active()
    {
        base.Active();
        hp = max_hp;
        GetComponent<Rigidbody>().isKinematic = false;
        this.transform.position = mStartPos;
    }
    //모든 어빌리티를 끔
    void DisableAbilities()
    {
        for (int i = 0; i < mAbilities.Count; i++)
        {
            mAbilities[i].StopAllCoroutines();
            mAbilities[i].enabled = false;
        }
    }

    void AbleAbilities()
    {
        for (int i = 0; i < mAbilities.Count; i++)
        {
            mAbilities[i].enabled = true;
        }
    }

    void SpawnCoin(Vector3 pos)
    {
        List<GameObject> mCoins = GameObject.Find("CoinManager").GetComponent<ObjectPooling>().obejcts;

        for (int i = 0; i < mCoins.Count; i++)
        {
            if (mCoins[i].active == false)
            {
                mCoins[i].transform.position = pos;
                mCoins[i].SetActive(true);
                break;
            }
        }
    }
    ///mAbilities리스트에 어빌리티 등록
    public void RegisterAbility(EnemyAbility ability)
    {
        if ((ability != GetComponent<EnemyAbility>())
            && (ability != GetComponent<EnemyMovement>()))
        {
            Debug.Log(ability);
            mAbilities.Add(ability);
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTime);
        Active();
    }
    //몇 초 뒤에 사라질 것인지
    IEnumerator ActiveFalseDelay()
    {
        yield return new WaitForSeconds(activeFalseTime);
        gameObject.SetActive(false);
    }

}
