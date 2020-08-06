using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug20195Library
{
	public class ResourceResolver
	{
		public static T ResolveTopLevelResource<T>(object key, T fallbackValue = default)
		{
			if (TryTopLevelRetrieval(key, context: null, out var value) && value is T tValue)
			{
				return tValue;
			}

			return fallbackValue;
		}
		internal static bool TryTopLevelRetrieval(object resourceKey, object context, out object value)
		{
			value = null;
			return false;
		}
	}

	public class MyLibraryTestClass
	{
		static int c_paneToggleButtonWidth = 48;

		public void Test()
		{
			var buttonSize = ResourceResolver.ResolveTopLevelResource<double>(key: "PaneToggleButtonWidth", fallbackValue: c_paneToggleButtonWidth);
		}
	}
}
