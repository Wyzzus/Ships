using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEntity : Entity
{
    public List<int> EnemyFractionsIDs;
    public List<int> FriendFractionsIDs;

    public override void Start()
    {
        Components = new List<EntityComponent>();
        GetAIComponents();
    }

    public virtual void GetAIComponents()
    {

    }

    public bool IsEnemy(int id)
    {
        if (EnemyFractionsIDs.Contains(id))
            return true;
        return false;
    }

    public bool IsFriend(int id)
    {
        if (FriendFractionsIDs.Contains(id))
            return true;
        return false;
    }
}