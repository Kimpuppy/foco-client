//	Copyright (c) KimPuppy.
//	http://bakak112.tistory.com/

using UnityEngine;

public class GhostSprite : MonoBehaviour
{
    public bool IsEnable = false;
    public bool IsSolid = false;
    public bool IsShake = false;

    public Color SpriteColor = Color.white;

    public float DrawTime = 0.01f;
    public float ShakeAmount = 0.1f;
    
    public float ScaleFactor = 1f;

    private SpriteRenderer objSprRender;
    private Material solidMat;

    private Timer drawTimer;

    private void Start()
    {
        objSprRender = GetComponent<SpriteRenderer>();
        solidMat = Resources.Load<Material>("Materials/Sprites-Solid");

        drawTimer = new Timer(DrawTime, true)
        {
            IsEnable = true
        };
    }

    private void Update()
    {
        if (IsEnable)
        {
            if (drawTimer.IsDone)
            {
                GameObject sprChild = new GameObject("@" + gameObject.name + "_GhostSprite", typeof(SpriteRenderer));
                SpriteRenderer childSprRender = sprChild.GetComponent<SpriteRenderer>();

                sprChild.transform.position = transform.position + new Vector3(0.0f, 0.0f, 0.001f);
                if (IsShake)
                    sprChild.transform.position += new Vector3(Random.Range(-ShakeAmount, ShakeAmount), Random.Range(-ShakeAmount, ShakeAmount));

                sprChild.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, transform.localRotation.eulerAngles.z);
                sprChild.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, 1.0f) * ScaleFactor;
                childSprRender.sprite = objSprRender.sprite;
                childSprRender.color = SpriteColor - new Color(0.0f, 0.0f, 0.0f, 0.8f);

                if (IsSolid)
                    childSprRender.material = solidMat;

                Destroy(sprChild, 0.2f);
            }
        }
    }
}