using UnityEngine;

public class StandartBrick : Brick
{
    public override void MinusLive(int power)
    {
        m_countHits -= power;
        if (m_countHits <= 0)
        {
            DestroyObject();
        }
    }
}
