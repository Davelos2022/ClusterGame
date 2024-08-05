using Cysharp.Threading.Tasks;
using MulticastProject.Models;
using MulticastProject.Data;
using MulticastProject.Utils;
using System.IO;

namespace MulticastProject.Core
{
    /// <summary>
    /// Class for progress game.
    /// </summary>
    public class PlayerManager
    {
        public PlayerData PlayerData { get; private set; }

        public async UniTask LoadProgressAsync()
        {
            string _pathToFile = Path.Combine(NameDataSystem.DIRECTORY_PLAYER, NameDataSystem.NAME_PLAYER_JSON_FILE);
            PlayerData = await JSONService.LoadJSONAsync<PlayerData>(_pathToFile, TypeTargetJson.StreamingAssets);

            if (PlayerData == null) PlayerData = new PlayerData();
        }

        public async UniTask SaveProgressAsync(PlayerData playerData)
        {
            PlayerData = playerData;
            await JSONService.SaveJSONAsync(NameDataSystem.DIRECTORY_PLAYER, NameDataSystem.NAME_PLAYER_JSON_FILE, PlayerData, TypeTargetJson.StreamingAssets);
        }
    }
}
