using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public static AudioManager Instance;

	public bool slowIntro = false;
	private float timeSinceStart = 0.0f;
	private bool musicPlaying = false;

	private float timeSinceExciting = 0.0f;
	private bool excitingPlaying = false;

	private float timeSinceDanger = 0.0f;
	private bool dangerPlaying = false;

	private float timeSinceInteresting = 0.0f;
	private bool interestingPlaying = false;

	private float timeSinceSlog = 0.0f;
	private bool slogPlaying = false;

	// Use this for initialization
	void Start () {
		Fabric.EventManager.Instance.PostEvent ("Music");
		Fabric.EventManager.Instance.SetParameter ("Music", "Hum", 1.0f, null);
		if (!slowIntro) {
			musicPlaying = true;
			Fabric.EventManager.Instance.SetParameter ("Music", "Base", 1.0f, null);
		}
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		float t = Time.deltaTime;
		timeSinceStart += t;
		timeSinceExciting += t;
		timeSinceDanger += t;
		timeSinceInteresting += t;
		timeSinceSlog += t;

		if (slowIntro && !musicPlaying && timeSinceStart > 4) {
			musicPlaying = true;
			Fabric.EventManager.Instance.SetParameter ("Music", "Base", 1.0f, null);
		}

		if (timeSinceExciting > 4 && excitingPlaying) {
			excitingPlaying = false;
			Fabric.EventManager.Instance.SetParameter ("Music", "Cymb", 0.0f, null);
		}

		if (timeSinceDanger > 4 && dangerPlaying) {
			dangerPlaying = false;
			Fabric.EventManager.Instance.SetParameter ("Music", "Main", 0.0f, null);
		}

		if (timeSinceInteresting > 4 && interestingPlaying) {
			interestingPlaying = false;
			Fabric.EventManager.Instance.SetParameter ("Music", "Spheric", 0.0f, null);
		}

		if (timeSinceSlog > 4 && slogPlaying) {
			slogPlaying = false;
			Fabric.EventManager.Instance.SetParameter ("Music", "Drums", 0.0f, null);
		}
	}

	public void Exciting() {
		timeSinceExciting = 0;
		if (!excitingPlaying) {
			Fabric.EventManager.Instance.SetParameter ("Music", "Cymb", 1.0f, null);
		}
	}

	public void Danger() {
		timeSinceDanger = 0;
		if (!dangerPlaying) {
			Fabric.EventManager.Instance.SetParameter ("Music", "Main", 1.0f, null);
		}
	}

	public void Interesting() {
		timeSinceInteresting = 0;
		if (!interestingPlaying) {
			Fabric.EventManager.Instance.SetParameter ("Music", "Spheric", 1.0f, null);
		}
	}

	public void Slog() {
		timeSinceSlog = 0;
		if (!slogPlaying) {
			Fabric.EventManager.Instance.SetParameter ("Music", "Drums", 1.0f, null);
		}
	}
}
