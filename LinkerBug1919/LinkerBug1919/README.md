# Repro for issue https://github.com/mono/linker/issues/1919

Reproducing this requires .NET 5 RC1 or later, mono and ninja installed.

Build with `dotnet build`

The SDK and linker binaries are located in %TMP/dotnet-runtime-wasm-XXX. 

The linker command line is provided when running `dotnet build /bl` in the `msbuild.binlog` file.