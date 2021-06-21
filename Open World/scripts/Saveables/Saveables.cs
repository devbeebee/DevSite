using System.IO;
using UnityEngine;
using static Collectables;

static class Saveables
{
    #region Collectables
    public static string CollectablesJsonPath => $"{GlobalStaticData.DocumentsDirWithAppName}/PlayerCollectables";
    public static CollectableList LoadCollectables(CollectableTypes collectableType) => File.Exists($"{CollectablesJsonPath}{collectableType}.json") ?
        JsonUtility.FromJson<CollectableList>(File.ReadAllText($"{CollectablesJsonPath}{collectableType}.json")) : new CollectableList(collectableType);

    public static void SaveCollectables(this CollectableList collectables) => File.WriteAllText($"{CollectablesJsonPath}{collectables.collectableType}.json", JsonUtility.ToJson(collectables));

    #endregion
    #region Experience   
    public static string ExperienceJsonPath => $"{GlobalStaticData.DocumentsDirWithAppName}/PlayerExp.json";
    public static Experience LoadExp() => File.Exists(ExperienceJsonPath) ? JsonUtility.FromJson<Experience>(File.ReadAllText(ExperienceJsonPath)) : new Experience();
    public static void SaveExperience(this Experience exp) => File.WriteAllText(ExperienceJsonPath, JsonUtility.ToJson(exp));
    #endregion
}
