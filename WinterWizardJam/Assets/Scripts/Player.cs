using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public static bool HandlingInput = true;

    private Checkpoint m_checkPoint;
    private Rigidbody2D m_rigidbody2D;
    private ConstantForce2D m_constantForce2D;

    private bool m_bButtonHeld;
    private bool m_bCasting;
    private float m_fMana = 100;

    private IcePath m_icePath;

    public float maxMana = 100;
    public float manaCostRate = 1;
    public float manaRechargeRate = 3;
    public float respawnTime = 3;
    public float spinForce = 100;
    public float minPathDistance = 15;
    public float minMagnitude = 1;


    public GameObject ClickParticle;

    public bool IsMouseButtonHeld
    {
        get
        {
            return HandlingInput && m_bButtonHeld;
        }
    }

    public float Mana
    {
        get { return m_fMana; }
    }

    public bool HasMana
    {
        get
        {
            return (m_fMana > 0);
        }
    }
 
    void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_constantForce2D = GetComponent<ConstantForce2D>();
        m_icePath = GameObject.Find("IcePath").GetComponent<IcePath>();
    }

    void Update()
    {
        if(m_fMana < 0)
        {
            m_fMana = 0;
        }

        if(m_fMana > maxMana)
        {
            m_fMana = maxMana;
        }

        if(m_bButtonHeld && (m_fMana > 0))
        {
            m_fMana -= manaCostRate * Time.deltaTime;

        }
        else if(m_fMana < maxMana && !m_bButtonHeld)
        {
            m_fMana += manaRechargeRate * Time.deltaTime;
        }

        if(HandlingInput && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(MouseButtonHeld());
            if (m_fMana > 0)
            {
                m_icePath.Cast();
            }
        }
    }

   IEnumerator MouseButtonHeld()
   {
        

        Vector2 startingPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        ClickParticle.Spawn(startingPoint);

       m_bButtonHeld = true;
       
       while(Input.GetMouseButton(0))
       {
            yield return null;
       }

       m_bButtonHeld = false;

        Vector2 endingPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float pathDistance = Vector2.Distance(startingPoint, endingPoint);
        Debug.Log("Path Distance: " + pathDistance);
        if (pathDistance> minPathDistance)
        {
            SetDirection(endingPoint);
            ClickParticle.Spawn(endingPoint);
        }
        else
        {
            m_icePath.CleanUp();
        }
   }

    private void SetDirection(Vector3 position)
    {
        if(position.x < transform.position.x)
        {
            m_rigidbody2D.AddTorque(spinForce);
        }
        else if(position.x > transform.position.x)
        {
            m_rigidbody2D.AddTorque(-spinForce);
        }
    }


    public void ResetPhysics()
    {
        m_rigidbody2D.velocity = Vector2.zero;
        m_rigidbody2D.angularVelocity = 0;
        m_constantForce2D.torque = 0;
    }

    public IEnumerator Respawn()
    {
        m_icePath.CleanUp();
        yield return new WaitForSeconds(respawnTime);

        if(m_checkPoint != null)
        {
            m_fMana = maxMana;
            transform.position = m_checkPoint.spawnPosition.position;
            m_rigidbody2D.velocity = Vector2.zero;
            m_rigidbody2D.angularVelocity = 0;
            m_constantForce2D.torque = 0;
        }
    }


    public void SetCheckpoint(Checkpoint checkPoint)
    {
        Debug.Log("Set Checkpoint.");
        if(m_checkPoint != null)
        {
            m_checkPoint.Deactivate();
        }

        m_checkPoint = checkPoint;
    }
}
