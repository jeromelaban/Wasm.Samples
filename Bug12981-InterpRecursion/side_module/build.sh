#!/bin/bash
emcc main.c -s MAIN_MODULE=1 -O2 -o hello.html -s WASM=1 -s EXPORT_ALL=1 --pre-js pre.js

emcc mysideModule.c -s SIDE_MODULE=1 -O2 -o side.wasm -s WASM=1 -s EXPORT_ALL=1
