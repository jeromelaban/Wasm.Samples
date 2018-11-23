## StructLayout tests using mono-wasm

This sample shows how to perform interop between C# and javascript, using JS eval() and structure marshalling using [`Marshal.StructureToPtr`](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.interopservices.marshal.structuretoptr?view=netframework-4.7.2).

The performance results for this sample, on an i7-7700 using the mono-wasm interpreter, on Windows 10 17134:
- Edge: `JSInvoke: 00:00:01.1145000 EM_JS: 00:00:01.2306000`
- Chrome: `JSInvoke: 00:00:02.2772999 EM_JS: 00:00:00.3217000`
- Firefox: `JSInvoke: 00:00:00.7680000 EM_JS: 00:00:00.1120000`