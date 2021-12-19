using UnityEngine;
using UnityEngine.Rendering;

public class GriRenderPipeline : RenderPipeline
{
    // 描画処理
    protected override void Render(ScriptableRenderContext context, Camera[] cameras)
    {
        // カメラごとに処理
        foreach (var camera in cameras)
        {
            // カメラプロパティ設定（カメラアングルの反映など）
            context.SetupCameraProperties(camera);
            
            // カリング処理
            CullingResults cullingResults = new CullingResults();
            ScriptableCullingParameters cullingParameters;
            if (camera.TryGetCullingParameters(false, out cullingParameters))
            {
                cullingResults = context.Cull(ref cullingParameters);
            }
            
            // フィルタリング＆ソート設定
            var sortingSettings = new SortingSettings(camera)
            {
                criteria = SortingCriteria.CommonOpaque
            };
            var drawingSettings = new DrawingSettings(new ShaderTagId("SRPDefaultUnlit"), sortingSettings);
            var filteringSettings = new FilteringSettings(RenderQueueRange.all);
            
            // 描画処理
            context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
            
            // SkyBoxの描画
            context.DrawSkybox(camera);
        }
        
        context.Submit();
    }
}
