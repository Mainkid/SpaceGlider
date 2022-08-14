using UnityEditor.Build.Reporting;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        
        
        private readonly IAssetProvider _assets;

        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }
        public GameObject CreateHero()=>
            _assets.Instantiate(AssetPath.HERO_PATH);

    }
}