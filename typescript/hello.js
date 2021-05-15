// hello world `
// console.log("Hello World");
function greet(person, date) {
    console.log(`Hello ${person}, today is ${date.toDateString()}`);
}
greet("hello", new Date());
function getFavoriteNUmber() {
    return 11;
}
// optional properties (?)
function printName(obj) {
    var _a;
    console.log(obj.firstName);
    console.log((_a = obj.lastName) === null || _a === void 0 ? void 0 : _a.toUpperCase());
    // or
    if (obj.lastName !== undefined) {
        console.log(obj.lastName);
    }
}
let message = "hello there !";
var x = getFavoriteNUmber();
function Pin12(pt) {
    console.log(`X is : ${pt.x}`);
    console.log(`Y is : ${pt.y}`);
}
Pin1({ x: 100, y: 200 });
function Pin1(pt) {
    console.log(`X is : ${pt.x}`);
    console.log(`Y is : ${pt.y}`);
}
Pin1({ x: 100, y: 200 });
