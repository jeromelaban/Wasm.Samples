# WebAssembly Roslyn sample using mono-wasm

How to build the sample:
- Using VS2017/2019:
	* Build the solution using the standard F5/Ctrl+F5
- Using the CLI (Linux, macOS)
	* Build the solution using `msbuild /r`
	* Got to the `bin/Debug/netstandard2.0/dist` folder
	* Run `python server.py`, navigate to `http://localhost:8000`


Once the page has loaded, the browser's debugging console will show the program's output.