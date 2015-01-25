using UnityEngine;

public class AdditivelyLoadLevels : UnitySingleton<AdditivelyLoadLevels>
{
	[SerializeField]
	string[] levelNames;

	// Use this for initialization
	void Start ()
	{
		foreach (string name in levelNames) {
			if (name == Application.loadedLevelName)
				continue;

			Application.LoadLevelAdditiveAsync (name);
		}
	}

}
