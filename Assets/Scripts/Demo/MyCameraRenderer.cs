using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.CustomRP.MyRP
{
    public class MyCameraRenderer
    {
        private ScriptableRenderContext context;
        private Camera camera;

        private CullingResults cullingResults;

        private static ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");
        
        public void Render(ScriptableRenderContext context, Camera camera)
        {
            this.context = context;
            this.camera = camera;

            if (!Cull())
            {
                return;
            }

            Setup();
            DrawVisibleGeometry();
            Submit();
        }

        private void DrawVisibleGeometry()
        {
            // 1.不透過オブジェクトだけ先に描画
            var sortingSettings = new SortingSettings(camera);
            var drawingSettings = new DrawingSettings(unlitShaderTagId, sortingSettings);
            var filteringSettings = new FilteringSettings(RenderQueueRange.all);
            context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);

            // 2.skyboxの描画
            context.DrawSkybox(camera);

            // 3.透過オブジェクトを描画
            sortingSettings.criteria = SortingCriteria.CommonTransparent;
            drawingSettings.sortingSettings = sortingSettings;
            filteringSettings.renderQueueRange = RenderQueueRange.transparent;
            
            context.DrawRenderers(cullingResults, ref drawingSettings, ref filteringSettings);
        }

        private void Submit()
        {
            context.Submit();
        }

        private void Setup()
        {
            // カメラアングルの反映
            context.SetupCameraProperties(camera);
        }

        private bool Cull()
        {
            if (camera.TryGetCullingParameters(out ScriptableCullingParameters p))
            {
                cullingResults = context.Cull(ref p);
                return true;
            }
            return false;
        }
    }
}

