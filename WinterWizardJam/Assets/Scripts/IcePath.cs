using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IcePath : MonoBehaviour {

    private readonly Vector2[] offscreen = new Vector2[2] { new Vector2(float.MaxValue, float.MaxValue), new Vector2(float.MaxValue - 1, float.MaxValue - 1) };

    private EdgeCollider2D m_edgeCollider2D;
    private List<IcePathNode> m_path = new List<IcePathNode>();
    private Player m_player;

    public float seconds;
    public IcePathNode icePathNodePrefab;
    public IceTrail iceTrailPrefab;

    public void CleanUp()
    {
        m_path.Clear();
        m_edgeCollider2D.points = offscreen;
    }

	void Awake () 
    {
        m_edgeCollider2D = GetComponent<EdgeCollider2D>();
        m_edgeCollider2D.points = offscreen;
        icePathNodePrefab.CreatePool(250);
        m_player = GameObject.Find("Player").GetComponent<Player>();
	}

    public void Cast()
    {
        m_path.Clear();
        m_edgeCollider2D.points = offscreen;
        StartCoroutine(CreatePath());
    }

    IEnumerator CreatePath()
    {
        iceTrailPrefab.Spawn();
        while (m_player.IsMouseButtonHeld && m_player.HasMana)
        {
            var icePathNode = icePathNodePrefab.Spawn();
            icePathNode.IcePathNodeDestroyed += IcePathNode_IcePathNodeDestroyed;
             
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            icePathNode.transform.position = pos;
            
            m_path.Add(icePathNode);

            if (m_path.Count > 1)
            {
                m_edgeCollider2D.points = PathPoints();
            }
            yield return new WaitForSeconds(seconds);
        }
    }

    Vector2[] PathPoints()
    {
        List<Vector2> v  = new List<Vector2>();
        for(int i=0; i < m_path.Count; i++)
        {
            v.Add(m_path[i].transform.position);
        }

        return v.ToArray();
    }

    private void IcePathNode_IcePathNodeDestroyed(IcePathNode icePathNode)
    {
        m_path.Remove(icePathNode);

        if (m_path.Count > 1)
        {
            m_edgeCollider2D.points = PathPoints();
        }
        else
        {
            m_edgeCollider2D.points = offscreen;
        }
    }
}
