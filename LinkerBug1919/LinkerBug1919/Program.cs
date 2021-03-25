using System;

namespace Uno.Wasm.Sample
{
	public static class Program
	{
		static void Main()
		{
			SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_sqlite3());

			Console.WriteLine(typeof(Microsoft.EntityFrameworkCore.DbContext));

			Console.WriteLine(SQLitePCL.raw.sqlite3_libversion_number().ToString());
		}
	}
}