using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//製作地圖
public class Mathfff : MonoBehaviour
{
    #region 欄位
    [Header("地圖座標放大倍數")]
    public float rage;

    /// <summary>
    /// 真實地圖
    /// </summary>
    public GameObject[] map =
       {
        new GameObject ("mapA"),
        new GameObject ("mapB"),
        new GameObject ("mapC"),
        new GameObject ("mapD"),
        new GameObject ("mapE"),
    };

    /// <summary>
    /// 地圖類型
    /// </summary>
    public GameObject[] smallMap =
       {
        new GameObject ("smallMapA"),
        new GameObject ("smallMapB"),
        new GameObject ("smallMapC"),
        new GameObject ("smallMapD"),
        new GameObject ("smallMapE"),
    };
    [Header("小地圖位置")]
    public Vector2[] smallMapAV2;
    public Vector2[] smallMapBV2;
    public Vector2[] smallMapCV2;
    public Vector2[] smallMapDV2;
    public Vector2[] smallMapEV2;

    //父物件
    public RectTransform canvas;
    //選擇生成的 地圖名字
    public string mapName;
    //小型地圖大小
    public float mapSize;
    //路障
    public GameObject forest;
    #endregion


    #region 方法
    private void BigMapMaker( Vector2 sMap)
    {
        //生成大地圖
        GameObject maps = Instantiate(map[0]);
        //地圖座標調整
        maps.transform.position = new Vector3(sMap.x * rage, sMap.y * rage, 0);
        //如果小地圖之間的射線 相隔甚遠
        RaycastHit2D hit2T = Physics2D.Raycast(sMap + new Vector2(-mapSize, mapSize), maps.transform.right, mapSize * 2, 1 << 9);
        RaycastHit2D hit2B = Physics2D.Raycast(sMap + new Vector2(-mapSize, -mapSize), maps.transform.right, mapSize * 2, 1 << 9);
        //生成路障
        if (hit2T.transform.tag != "smallMap")
        {
            Instantiate(forest, maps.transform);
            maps.transform.position += new Vector3(0, mapSize, 0);
        }
        if (hit2B.transform.tag != "smallMap")
        {
            Instantiate(forest, maps.transform);
            maps.transform.position -= new Vector3(0, mapSize, 0);
        }
        //如果小地圖之間的射線 相隔甚遠
        RaycastHit2D hit2R = Physics2D.Raycast(sMap + new Vector2(mapSize, -mapSize), maps.transform.up, mapSize * 2, 1 << 9);
        RaycastHit2D hit2L = Physics2D.Raycast(sMap + new Vector2(-mapSize, -mapSize), maps.transform.up, mapSize * 2, 1 << 9);
        //生成路障
        if (hit2R.transform.tag != "smallMap")
        {
            Instantiate(forest, maps.transform);
            maps.transform.position += new Vector3(mapSize, 0, 0);
        }
        if (hit2L.transform.tag != "smallMap")
        {
            Instantiate(forest, maps.transform);
            maps.transform.position -= new Vector3(mapSize, 0, 0);
        }
    }
    #endregion

    #region 事件
    /// <summary>
    /// 地圖選項
    /// </summary>
    public void MapOptions()
    {
        //小地圖選項 圖層
        RaycastHit2D map = Physics2D.Raycast(Input.GetTouch(0).position, transform.up, 0.1f, 1 << 10);
        string mapName = map.transform.name;
    }

    private void OnPointerDown()
    {
        RaycastHit2D mapLF = Physics2D.Raycast(Input.GetTouch(0).position + new Vector2(-mapSize, mapSize), transform.up, 0.1f);
        RaycastHit2D mapLB = Physics2D.Raycast(Input.GetTouch(0).position + new Vector2(-mapSize, -mapSize), transform.up, 0.1f);
        RaycastHit2D mapRF = Physics2D.Raycast(Input.GetTouch(0).position + new Vector2(mapSize, mapSize), transform.up, 0.1f);
        RaycastHit2D mapRB = Physics2D.Raycast(Input.GetTouch(0).position + new Vector2(mapSize, -mapSize), transform.up, 0.1f);

        //如果 四角沒有物件擋，就生成
        if (!mapLF || mapLB || mapRF || mapRB)
        {
            if (mapName == smallMap[0].name)
            {
                //產生(小地圖,父物件)
                GameObject smallA = Instantiate(smallMap[0], canvas);
                //小地圖的座標=點擊座標
                smallA.GetComponent<RectTransform>().anchoredPosition = Input.GetTouch(0).position;
                //紀錄生成後的 座標
                Vector2[] smallMapAV2 = { (Vector2)(smallA.transform.position) };
            }
            if (mapName == smallMap[1].name)
            {
                GameObject smallB = Instantiate(smallMap[1], canvas);
                smallB.GetComponent<RectTransform>().anchoredPosition = Input.GetTouch(0).position;
                //紀錄生成後的 座標
                Vector2[] smallMapBV2 = { (Vector2)(smallB.transform.position) };
            }
            if (mapName == smallMap[2].name)
            {
                GameObject smallC = Instantiate(smallMap[2], canvas);
                smallC.GetComponent<RectTransform>().anchoredPosition = Input.GetTouch(0).position;
                //紀錄生成後的 座標
                Vector2[] smallMapCV2 = { (Vector2)(smallC.transform.position) };
            }
            if (mapName == smallMap[3].name)
            {
                GameObject smallD = Instantiate(smallMap[3], canvas);
                smallD.GetComponent<RectTransform>().anchoredPosition = Input.GetTouch(0).position;
                //紀錄生成後的 座標
                Vector2[] smallMapDV2 = { (Vector2)(smallD.transform.position) };
            }
            if (mapName == smallMap[4].name)
            {
                GameObject smallE = Instantiate(smallMap[4], canvas);
                smallE.GetComponent<RectTransform>().anchoredPosition = Input.GetTouch(0).position;
                //紀錄生成後的 座標
                Vector2[] smallMapEV2 = { (Vector2)(smallE.transform.position) };
            }
            if (mapName == "clear")
            {
                RaycastHit2D mapDestroy = Physics2D.Raycast(Input.GetTouch(0).position, transform.up, 0, 1 << 9);
                Destroy(mapDestroy.transform.gameObject);
            }
        }
    }

    private void OnMove()
    {
        //檢測點選物件是不是 小地圖
        RaycastHit2D hit2DF = Physics2D.Raycast(Input.GetTouch(0).position, transform.up, 0, 1 << 9);
        if (hit2DF.transform.parent.tag == "smallMap" || Input.GetTouch(0).position.y < 700)
        {
            //檢測四角有無阻擋
            RaycastHit2D mapLF = Physics2D.Raycast((Vector2)hit2DF.transform.position + new Vector2(-mapSize, mapSize), transform.up, 0.1f);
            RaycastHit2D mapLB = Physics2D.Raycast((Vector2)hit2DF.transform.position + new Vector2(-mapSize, -mapSize), transform.up, 0.1f);
            RaycastHit2D mapRF = Physics2D.Raycast((Vector2)hit2DF.transform.position + new Vector2(mapSize, mapSize), transform.up, 0.1f);
            RaycastHit2D mapRB = Physics2D.Raycast((Vector2)hit2DF.transform.position + new Vector2(mapSize, -mapSize), transform.up, 0.1f);
            if (!mapLF || mapLB || mapRF || mapRB)
            {
                //小地圖的座標=點擊座標
                hit2DF.transform.position = Input.GetTouch(0).position;
            }
        }
    }

    private void EnterGame()
    {
        foreach (Vector2 sMap in smallMapAV2) BigMapMaker(sMap);
        foreach (Vector2 sMap in smallMapBV2) BigMapMaker(sMap);
        foreach (Vector2 sMap in smallMapCV2) BigMapMaker(sMap);
        foreach (Vector2 sMap in smallMapDV2) BigMapMaker(sMap);
        foreach (Vector2 sMap in smallMapEV2) BigMapMaker(sMap);
    }
    #endregion

}
