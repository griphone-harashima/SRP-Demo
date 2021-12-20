using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/GriRenderPipelineComplete")]
public class GriRenderPipelineAssetComplete : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        return new GriRenderPipelineComplete();
    }
}
