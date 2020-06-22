using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLerper : MonoBehaviour
{
  Tilemap thisSprite;
  [SerializeField] [Range(0f, 1f)] float lerpTime = 0;
  [SerializeField] Color[] myColors;
  int colorIndex = 0;
  float t = 0f;
  int len;

  // Start is called before the first frame update
  void Start()
  {
    thisSprite = GetComponent<Tilemap>();
    len = myColors.Length;
  }

  // Update is called once per frame
  void Update()
  {
    thisSprite.color = Color.Lerp(thisSprite.color, myColors[colorIndex], lerpTime*Time.deltaTime);
    t = Mathf.Lerp(t, 1f, lerpTime*Time.deltaTime);
    if(t > .9f){
      t = 0f;
      colorIndex++;
      colorIndex = ( colorIndex >= len) ? 0 : colorIndex;
    }

  }
}
