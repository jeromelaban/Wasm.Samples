#include <stdlib.h>
#include <string.h>
#include <stdio.h>

#define WASM_EXPORT __attribute__((visibility("default")))

WASM_EXPORT int test_add(int a, int b) {
	printf("test_add(%d, %d)", a, b);

	return a + b;
}
