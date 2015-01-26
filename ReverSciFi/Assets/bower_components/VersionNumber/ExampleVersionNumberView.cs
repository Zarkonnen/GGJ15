using UnityEngine;
using Net.Xeophin.Utils.Interfaces;

namespace Net.Xeophin.Utils.Version
{
		/// <summary>
		/// Demonstration implementation of the ILabelView.
		/// </summary>
		/// <description>
		/// This component can be attached to a game object. It will then
		/// show the version number in the bottom left corner of the screen.
		/// </description>
		public class ExampleVersionNumberView : MonoBehaviour, ILabelView
		{
				/// <summary>
				/// Can be set to true, in that case the version number will be shown in bottom right of the screen
				/// </summary>
				[SerializeField]
    #if UNITY_4_5
				[Tooltip ("Can be set to true, in that case the version number will be shown in bottom right of the screen.")]
				#endif
    bool showVersionInformation = false;
				/// <summary>
				/// Show the version during the first 20 seconds.
				/// </summary>
				[SerializeField]
    #if UNITY_4_5
				[Tooltip ("Show the version during the first 20 seconds.")]
				#endif
    bool showVersionDuringTheFirst20Seconds = true;

				/// <summary>
				/// The position of the version information.
				/// </summary>
				Rect position = new Rect (0, 0, 100, 20);

				protected VersionNumberController Controller;

				#region MonoBehaviour

				void Start ()
				{
						DontDestroyOnLoad (this);

						// Initiate the controller
						Controller = new VersionNumberController (this);
  
						// Log current version in log file
						Debug.Log (string.Format ("[VersionNumber] Currently running version is {0}", Label));
  
						// Make the component autodestruct
						if (showVersionDuringTheFirst20Seconds) {
								showVersionInformation = true;
								Destroy (this, 20f);
						}

						// Set position of the GUI rect
						position.x = 10f;
						position.y = Screen.height - position.height - 10f;
  
				}


				void OnGUI ()
				{
						if (!showVersionInformation) {
								return;
						}
        
						GUI.contentColor = Color.gray;
						GUI.Label (position, string.Format ("v{0}", Label));
				}

				#endregion

				#region ILabelView implementation

				/// <summary>
				/// Sets the label.
				/// </summary>
				/// <value>The label.</value>
				public string Label { set; private get; }

				#endregion
		}




}

