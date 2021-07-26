// JavaScript benchmark
var randomList = [...Array(500000).keys()].map(_ => Math.random());
var randomSum = 0;
var startTime = Date.now();

for (var i = 0; i < randomList.length; i++) {
    randomSum += randomList[i];
}

var finishTime = Date.now();
console.log(randomSum);
console.log(`Took ${finishTime - startTime} ms`);
