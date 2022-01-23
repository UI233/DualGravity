using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : Item
{
    public int bonusType;
    public Animator anim;
    // Start is called before the first frame update
    new private void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    new protected void Update()
    {
        base.Update();
    }

    private IEnumerator BaseDisapear(float time)
    {
        yield return new WaitForSeconds(time);
        collider.enabled = true;
        base.Disapear();
    }
    new public void Disapear()
    {
        collider.enabled = false;
        anim.SetTrigger("Disapear");
        StartCoroutine(BaseDisapear(1.65f));
    }
}
