using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sprite : MonoBehaviour {

    public Texture2D[] textures;
    public int currentTexture = 0;
    public Dictionary<string, int[]> Animations = new Dictionary<string, int[]>();
    public string CurrentAnimation;
    public float timeInterval = 0;
    public bool loop = true;
    private bool FacingLeft_;
    public bool FacingLeft
    {
        get
        {
            return FacingLeft_;
        }
        set
        {
            if (FacingLeft_ == value)
            {
                return;
            }
            FacingLeft_ = value;
            var local = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(-local.x, local.y, local.z);
        }
    }


    public void NextTexture()
    {
        currentTexture++;
        if ((currentTexture >= textures.Length) || currentTexture >= Animations[CurrentAnimation].Length) currentTexture = 0;
        this.gameObject.renderer.material.mainTexture = GetFrame(Animations[CurrentAnimation][currentTexture]);
        if ((currentTexture == textures.Length - 1) && loop)
        {
            StartCoroutine(TextureChanger());
        }
        else if((currentTexture == textures.Length - 1) && !loop) 
        {
           
        }
        else
        {
            StartCoroutine(TextureChanger());
        }
    }

    public Texture2D GetFrame(int i)
    {
        return textures[i];
    }

    public void PlayAnimation(string animationName, bool loop)
    {
        if(Animations.ContainsKey(animationName))
        {
            CurrentAnimation = animationName;
        }
    }
    
    IEnumerator TextureChanger(){
        yield return new WaitForSeconds(timeInterval);
        if(CurrentAnimation != null) NextTexture();
    }

	// Use this for initialization
	void Start () {
        this.gameObject.renderer.material.mainTexture = GetFrame(Animations[CurrentAnimation][currentTexture]);
        StartCoroutine(TextureChanger());
	}
	
	// Update is called once per frame
	void Update () {
        

       

	}
}
