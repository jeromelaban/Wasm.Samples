## Building the sample

- Create an interactive container with https://hub.docker.com/repository/docker/unoplatform/wasm-build tag 2.2 or 2.3.
- Build in the wasm folder using `msbuild /r /bl /p:Configuration=Release`
- server with `python3 server.py` in `bin\Release\netstandard2.0\dist`
