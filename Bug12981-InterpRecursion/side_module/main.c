#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <dlfcn.h>

int test_add(int a, int b);
typedef int (*test_add_func)(int, int);

int main(int argc, char** argv) {
	printf("test!\r\n");
	int r = test_add(21,21);
	printf("Result: %d\r\n", r);

	void* handle1 = dlopen("invalid.wasm", 0);
	void* handle2 = dlopen("invalid.wasm", 0);

	printf("Invalid: %d %d\n", hModule1, hModule2);

	void* hModule = dlopen("side.wasm", 0);
	test_add_func test_add_ptr = dlsym(hModule, "test_add");
	int res = test_add_ptr(22, 22);

	printf("Result: %d %d %d\r\n", hModule, test_add_ptr, res);
}

