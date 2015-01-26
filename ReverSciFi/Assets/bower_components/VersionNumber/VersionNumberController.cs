using System.Reflection;
using Net.Xeophin.Utils.Interfaces;

namespace Net.Xeophin.Utils.Version
{

		/// <summary>
		/// Version number controller.
		/// </summary>
		/// <description>
		/// This class will get the version number of the current active 
		/// assembly and provide that in a property or push it to the 
		/// label of an ILabelView, if provided.
		/// </description>
		/// 
		/// \version 1.0.0
		/// \author Kaspar Manz <code@xeophin.net>
		public class VersionNumberController
		{

				ILabelView view;


				/// <summary>
				/// Gets or sets the view.
				/// </summary>
				/// <value>The view.</value>
				public ILabelView View {
						private get {
								return view;
						}
						set {
								view = value;
								view.Label = Version;
						}
				}


				public VersionNumberController ()
				{
      
				}


				public VersionNumberController (ILabelView view)
				{
						this.View = view;
				}


				string version;


				/// <summary>
				/// Gets the version.
				/// </summary>
				/// <value>The version.</value>
				public string Version {
						get {
								if (version == null) {
										System.Version v = Assembly.GetExecutingAssembly ().GetName ().Version;
										version = string.Format ("{0}.{1}.{2}", v.Major, v.Minor, v.Build);
								}
								return version;
						}
				}
		}

}
