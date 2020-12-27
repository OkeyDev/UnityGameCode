using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    public SpriteRenderer BackObj;
    // Start is called before the first frame update
    void Start()
    {
        float worldScreenHeight = (float) (Camera.main.orthographicSize * 2.0);
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;
        BackObj.transform.localScale = new Vector2(BackObj.sprite.bounds.size.x / worldScreenWidth, BackObj.sprite.bounds.size.y / worldScreenHeight);
        //GameBackground.transform.localScale = n2ew Vector2 (); echo
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
