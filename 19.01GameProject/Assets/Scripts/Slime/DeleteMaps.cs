using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteMaps : EnemyAbility
{
    public GameObject DeleteMapObject = null;//삭제할 맵
    public float DelayDeleteMapTime = 3.0f; //지연 시간

    private float mDeleteTime = 0.0f;
    private bool bDeleteMap = false; //true일 경우 시간이 지나감

    private void OnEnable()
    {
        DeleteMap();
    }

    private void Update()
    {
        if(bDeleteMap == true)
        {
            mDeleteTime += Time.deltaTime;
            if(mDeleteTime > DelayDeleteMapTime)
            {
                bDeleteMap = false;
                mDeleteTime = 0.0f;
                DeleteMapObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 맵 삭제 함수 삭제할 맵을 붉은 색으로 전환, 카운트 다운 할 수 있도록 불 변수 true
    /// </summary>
    private void DeleteMap()
    {
        DeleteMapObject.GetComponent<Renderer>().material.color = Color.red;
        bDeleteMap = true;
        //닿을 경우 대미지 주는 스크립트를 오브젝트에 추가
    }
}
