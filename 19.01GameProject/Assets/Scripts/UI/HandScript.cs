using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    public Skill movingSkill;
    public string objectname;
    private static HandScript instance;

    [SerializeField]
    private Vector3 mOffset;

    private Image mIcon;

    public static HandScript MyInstance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }
            return instance;
        }        
    }

    public IMoveable MyMoveable { get; set; }

    public void TakeMoveable(IMoveable moveable)
    {
        this.MyMoveable = moveable;

        mIcon.sprite = moveable.MyIcon;
        mIcon.color = Color.white;
    }

    public void Drop()
    {
        movingSkill = null;
        MyMoveable = null;

        mIcon.color = new Color(0, 0, 0, 0);
    }

    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;

        movingSkill = null;

        MyMoveable = null;

        mIcon.color = new Color(0, 0, 0, 0);

        return tmp;
    }
    private void Start()
    {
        mIcon = GetComponent<Image>();
    }

    private void Update()
    {
        mIcon.transform.position = Input.mousePosition + mOffset;
    }
}
