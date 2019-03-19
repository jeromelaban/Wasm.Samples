var methodMap = [];

window.DotNet = {
    jsCallDispatcher: {
        findJSFunction: function (identifier) {
            if (identifier) {
                var parts = identifier.split(':');

                if (parts.length === 1) {
                    return window[identifier];
                }
                else {
                    var id = methodMap.length;
                    methodMap[id] = window[parts[1]];
                    return function () { return size; };
                }
            }
            else {
                return dispatch;
            }
        }
    }
};

function dispatch(id, params) {
    return methodMap[id](params);
}

function showResult(message) {
    var para = document.createElement("h1");
    var node = document.createTextNode(message);
    para.appendChild(node);
    document.body.appendChild(para);
}


function testLayoutSlow(params) {
    return params.id;
}

window.testLayoutFast = (pParams) => {

    var htmlId = Module.getValue(pParams, "*");
    pParams += 4;

    var pTagName = Module.getValue(pParams, "*");
    var tagName = Module.UTF8ToString(pTagName);
    pParams += 4;

    var handle = Module.getValue(pParams, "*");
    pParams += 4;

    var type = Module.UTF8ToString(Module.getValue(pParams, "*"));
    pParams += 4;

    var isSvg = Module.getValue(pParams, "i8");
    pParams += 4;

    var isFrameworkElement = Module.getValue(pParams, "i8");
    pParams += 4;

    var isFocusable = Module.getValue(pParams, "i8");
    pParams += 4;

    var classesCount = Module.getValue(pParams, "i32");
    pParams += 4;

    var classesPtr = Module.getValue(pParams, "*");

    var classes = [];
    for (var i = 0; i < classesCount; i++) {
        classes.push(MonoRuntime.conv_string(Module.getValue(classesPtr + i * 4, "*")));
    }

    return htmlId;
}