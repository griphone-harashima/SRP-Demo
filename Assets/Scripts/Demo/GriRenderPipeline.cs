using UnityEngine;
using UnityEngine.Rendering;

public class GriRenderPipeline : RenderPipeline
{
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        foreach (var camera in cameras)
        {
            // TODO:カメラプロパティの設定（カメラアングルの反映など）
            
            // TODO:カリング処理

            // TODO:フィルタリング＆ソート設定

            // TODO:描画処理
            
            // TODO:skyboxの描画
        }
        
        //レンダーターゲットに書き込む
        context.Submit();
    }
}
