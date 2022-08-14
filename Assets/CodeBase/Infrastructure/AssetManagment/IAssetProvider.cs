using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public interface IAssetProvider: IService
    {
        GameObject Instantiate(string path);
    }
}