using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlatformLerper : MonoBehaviour
{
  Tilemap thisTilemap;
  [SerializeField] [Range(0f, 1f)] float lerpTime = 0;
  [SerializeField] Color[] myColors;
  int colorIndex = 0;
  float t = 0f;
  int len;

  // Start is called before the first frame update
  void Start()
  {
    thisTilemap = GetComponent<Tilemap>();
    len = myColors.Length;
  }

  // Update is called once per frame
  void Update()
  {

    BoundsInt bounds = thisTilemap.cellBounds;
    TileBase[] allTiles = thisTilemap.GetTilesBlock(bounds);
    for (int x = 0; x < bounds.size.x; x++) {
        for (int y = 0; y < bounds.size.y; y++) {
            TileBase tile = allTiles[x + y * bounds.size.x];
            if (tile != null) {
                //Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
            } else {
                //Debug.Log("x:" + x + " y:" + y + " tile: (null)");
            }
        }
    }

    //thisTilemap.setColor = Color.Lerp(thisTilemap.color, myColors[colorIndex], lerpTime*Time.deltaTime);

    t = Mathf.Lerp(t, 1f, lerpTime*Time.deltaTime);
    if(t > .9f){
      t = 0f;
      colorIndex++;
      colorIndex = ( colorIndex >= len) ? 0 : colorIndex;
    }

  }
}
