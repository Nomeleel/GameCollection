using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBird : Bird
{
    public List<Pig> Objects = new List<Pig>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Pig>() != null)
        {
            Objects.Add(collision.gameObject.GetComponent<Pig>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Pig>() != null)
        {
            Objects.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }

    protected override void ShowSkill()
    {
        if (Objects != null)
        {
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].Dead();
            }
        }
    }

}
