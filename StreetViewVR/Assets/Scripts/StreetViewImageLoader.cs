using UnityEngine;
using System.Collections;

/// <summary>
/// Google Street Viewの画像を取得してSkyboxに設定する
/// </summary>
public class StreetViewImageLoader : MonoBehaviour
{
    public Material   material;
    private int       width  = 640;
    private int       height = 640;
    private double longitude = 139.7449067;
    private double latitude  = 35.6592487;
    private int       fov    = 90;
    private Texture2D frontTex, leftTex, rightTex, backTex, upTex, downTex;

    /// <summary>
    /// 初期化処理を行う
    /// </summary>
    void Start()
    {
        StartCoroutine(GetStreetViewImage(latitude, longitude, 0,   0,  fov)); //前
        StartCoroutine(GetStreetViewImage(latitude, longitude, 90,  0,  fov)); //右
        StartCoroutine(GetStreetViewImage(latitude, longitude, 180, 0,  fov)); //後
        StartCoroutine(GetStreetViewImage(latitude, longitude, 270, 0,  fov)); //左
        StartCoroutine(GetStreetViewImage(latitude, longitude, 0,   90, fov)); //上
        StartCoroutine(GetStreetViewImage(latitude, longitude, 0,  -90, fov)); //下
                                                                             
        StartCoroutine(WaitTime());
    }

    /// <summary>
    /// 前後左右上下の画像を取得する
    /// </summary>
    /// <param name="latitude">緯度</param>
    /// <param name="longitude">経度</param>
    /// <param name="heading">表示する方向（0,360:北、90:東、180:南、270:西）</param>
    /// <param name="pitch">カメラの上下の角度</param>
    /// <param name="fov">カメラのズーム（0～120。0が最もズームした値）</param>
    private IEnumerator GetStreetViewImage(double latitude, double longitude, int heading, int pitch, int fov)
    {
        string url = "http://maps.googleapis.com/maps/api/streetview?" + "size=" + width +
            "x" + height + "&location=" + latitude + "," + longitude + "&heading=" +
            heading + "&pitch=" + pitch + "&fov=" + fov + "&sensor=false";

        WWW www = new WWW(url);
        yield return www;
        www.texture.wrapMode = TextureWrapMode.Clamp;
        if (heading == 0)
        {
            if (pitch == 0)
            {
                frontTex = www.texture;
            }
            else if (pitch == 90)
            {
                upTex = www.texture;
            }
            else if (pitch == -90)
            {
                downTex = www.texture;
            }
        }
        else if (heading == 90)
        {
            leftTex = www.texture;
        }
        else if (heading == 180)
        {
            backTex = www.texture;
        }
        else if (heading == 270)
        {
            rightTex = www.texture;
        }
    }

    /// <summary>
    /// マテリアル設定前にウェイトを入れる
    /// </summary>
    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(1f);
        setSkybox();
    }

    /// <summary>
    /// skyboxのマテリアルを設定する
    /// </summary>
    private void setSkybox()
    {
        material.SetTexture("_FrontTex", frontTex);
        material.SetTexture("_BackTex", backTex);
        material.SetTexture("_LeftTex", leftTex);
        material.SetTexture("_RightTex", rightTex);
        material.SetTexture("_UpTex", upTex);
        material.SetTexture("_DownTex", downTex);

        RenderSettings.skybox = material;
    }
}