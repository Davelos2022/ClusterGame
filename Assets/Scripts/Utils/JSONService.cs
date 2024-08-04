using Newtonsoft.Json;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.IO;
using System;

namespace MulticastProject.Utils
{
    public enum TypeTargetJson
    {
        Resources,
        StreamingAssets
    }

    public static class JSONService
    {
        /// <summary>
        /// Loads JSON data from resources or file depending on the typeLoader.
        /// </summary>
        /// <typeparam name="T">The type to which the JSON data should be deserialized.</typeparam>
        /// <param name="pathToFile">The path to the JSON file.</param>
        /// <param name="typeLoader">The type of loader to use (Resources or File).</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains
        public static async UniTask<T> LoadJSONAsync<T>(string pathToFile, TypeTargetJson typeLoader)
        {
            string jsonData;

            try
            {
                switch (typeLoader)
                {
                    case TypeTargetJson.Resources:
                        jsonData = await LoadFromResourcesAsync(pathToFile);
                        break;
                    case TypeTargetJson.StreamingAssets:
                        jsonData = await LoadFromFileAsync(pathToFile);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(typeLoader), typeLoader, null);
                }

                if (!string.IsNullOrEmpty(jsonData))
                {
                    return JsonConvert.DeserializeObject<T>(jsonData);
                }
                else
                {
                    Debug.LogError("Failed to load or parse data!");
                    return default;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Exception occurred: {ex.Message}");
                return default;
            }
        }

        /// <summary>
        /// Saves JSON data to a specified file within the Resources folder.
        /// </summary>
        /// <param name="fileName">Name of the JSON file.</param>
        /// <param name="data">Object to be serialized and saved.</param>
        public static async UniTask SaveJSONAsync(string pathToDirectory, string fileName, object data, TypeTargetJson typeTargetJson)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                Debug.LogWarning("File name cannot be null or empty.");
                return;
            }

            if (typeTargetJson == TypeTargetJson.Resources)
                pathToDirectory = Path.Combine(Application.dataPath, "Resources", pathToDirectory);
            else if (typeTargetJson == TypeTargetJson.StreamingAssets)
                pathToDirectory = Path.Combine(Application.streamingAssetsPath, pathToDirectory);

            await CreateDirectoryIfNotExistsAsync(pathToDirectory);
            string filePath = Path.Combine(pathToDirectory, fileName + ".json");

            string json = JsonConvert.SerializeObject(data, Formatting.Indented);

            try
            {
                await File.WriteAllTextAsync(filePath, json);
                Debug.Log($"Configuration saved to {filePath}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to save configuration: {ex.Message}");
            }
        }

        /// <summary>
        /// Loads JSON data from resources.
        /// </summary>
        /// <param name="filePath">The path to the JSON file within the Resources folder.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the JSON data as a string.</returns>
        private static async UniTask<string> LoadFromResourcesAsync(string filePath)
        {
            TextAsset textAsset = await Resources.LoadAsync<TextAsset>(filePath) as TextAsset;

            if (textAsset != null)
            {
                return textAsset.text;
            }
            else
            {
                Debug.LogError("Resource file not found at: " + filePath);
                return string.Empty;
            }
        }

        /// <summary>
        /// Loads JSON data from a file is streaming assets.
        /// </summary>
        /// <param name="filePath">The path to the JSON file</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the JSON data as a string.</returns>
        private static async UniTask<string> LoadFromFileAsync(string filePath)
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, filePath + ".json");

            if (File.Exists(fullPath))
            {
                return await File.ReadAllTextAsync(fullPath);
            }
            else
            {
                Debug.Log("File not found at: " + fullPath);
                return string.Empty;
            }
        }

        private static async UniTask CreateDirectoryIfNotExistsAsync(string directory)
        {
            if (!Directory.Exists(directory))
            {
                await UniTask.RunOnThreadPool(() => Directory.CreateDirectory(directory));
                Debug.Log($"Directory created at: {directory}");
            }
        }
    }
}
