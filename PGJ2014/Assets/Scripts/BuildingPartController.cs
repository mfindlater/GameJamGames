using UnityEngine;
using System.Collections;

public enum BuildingMaterialType
{
    NONE = 0,
    GLASS,
    CONCRETE,
    STEEL
};

public class BuildingPartController : MonoBehaviour {
    public BuildingMaterialType materialType;
    public Sprite buildingPartSpriteLeftDamaged;
    public Sprite buildingPartSpriteRightDamaged;
    public Sprite buildingPartSpriteBothDamaged;
    public Sprite buildingPartSpriteLeftGoneRightNormal;
    public Sprite buildingPartSpriteLeftGoneRightDamaged;
    public Sprite buildingPartSpriteLeftNormalRightGone;
    public Sprite buildingPartSpriteLeftDamagedRightGone;

    public ParticleSystem leftParticleSystem;
    public ParticleSystem rightParticleSystem;

    private SpriteRenderer spriteRenderer;

    private int buildingLeftHP = 1;
    private int buildingRightHP = 1;
    private int buildingMaxHP = 1;

	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (materialType == BuildingMaterialType.GLASS)
        {
            buildingLeftHP = 2;
            buildingRightHP = 2;
            buildingMaxHP = 2;
        }
        else if (materialType == BuildingMaterialType.CONCRETE)
        {
            buildingLeftHP = 3;
            buildingRightHP = 3;
            buildingMaxHP = 3;
        }
        else if (materialType == BuildingMaterialType.STEEL)
        {
            buildingLeftHP = 4;
            buildingRightHP = 4;
            buildingMaxHP = 4;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DamageLeft(ScoreManager targetScoreManager)
    {
        if (buildingLeftHP > 0)
        {
            if (buildingRightHP >= buildingMaxHP)
            {
                if (buildingLeftHP == 1)
                {
                    spriteRenderer.sprite = buildingPartSpriteLeftGoneRightNormal;
                    targetScoreManager.AddScore(10 * buildingMaxHP);
                }
                else
                {
                    spriteRenderer.sprite = buildingPartSpriteLeftDamaged;
                }
                leftParticleSystem.Play();
            }
            else if (buildingRightHP <= 0)
            {
                if (buildingLeftHP == 1)
                {
                    leftParticleSystem.Play();
                    targetScoreManager.AddScore(10 * buildingMaxHP);
                    targetScoreManager.AddScore(100 * buildingMaxHP);
                    Destroy(gameObject);
                }
                else
                {
                    spriteRenderer.sprite = buildingPartSpriteLeftDamagedRightGone;
                    leftParticleSystem.Play();
                }
            }
            else
            {
                if (buildingLeftHP == 1)
                {
                    spriteRenderer.sprite = buildingPartSpriteLeftGoneRightDamaged;
                    targetScoreManager.AddScore(10 * buildingMaxHP);
                }
                else
                {
                    spriteRenderer.sprite = buildingPartSpriteBothDamaged;
                }
                leftParticleSystem.Play();
            }
            buildingLeftHP--;
        }
        //Debug.Log("Left HP: " + buildingLeftHP);
    }

    public void DamageRight(ScoreManager targetScoreManager)
    {
        if (buildingRightHP > 0)
        {
            if (buildingLeftHP >= buildingMaxHP)
            {
                if (buildingRightHP == 1)
                {
                    spriteRenderer.sprite = buildingPartSpriteLeftNormalRightGone;
                    targetScoreManager.AddScore(10 * buildingMaxHP);
                }
                else
                {
                    spriteRenderer.sprite = buildingPartSpriteRightDamaged;
                }
                rightParticleSystem.Play();
            }
            else if (buildingLeftHP <= 0)
            {
                if (buildingRightHP == 1)
                {
                    rightParticleSystem.Play();
                    targetScoreManager.AddScore(10 * buildingMaxHP);
                    targetScoreManager.AddScore(100 * buildingMaxHP);
                    Destroy(gameObject);
                }
                else
                {
                    spriteRenderer.sprite = buildingPartSpriteLeftGoneRightDamaged;
                    rightParticleSystem.Play();
                }
            }
            else
            {
                if (buildingRightHP == 1)
                {
                    spriteRenderer.sprite = buildingPartSpriteLeftDamagedRightGone;
                    targetScoreManager.AddScore(10 * buildingMaxHP);
                }
                else
                {
                    spriteRenderer.sprite = buildingPartSpriteBothDamaged;
                }
                rightParticleSystem.Play();
            }
            buildingRightHP--;
        }
        //Debug.Log("Right HP: " + buildingRightHP);
    }
}
