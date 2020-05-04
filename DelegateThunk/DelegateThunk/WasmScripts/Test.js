
function testInvoke(thunk) {
    var methodAddress = parseInt(thunk);

    Runtime.dynCall('v', methodAddress, []);
}
