// hello world `
// console.log("Hello World");

function greet(person: string, date: Date) {
    console.log(`Hello ${person}, today is ${date.toDateString()}`);
}

greet("hello", new Date());

function getFavoriteNUmber(): number {
    return 11;
}

// optional properties (?)
function printName(obj: { firstName: string, lastName?: string }) {
    console.log(obj.firstName);
    console.log(obj.lastName?.toUpperCase());
    // or
    if (obj.lastName !== undefined) {
        console.log(obj.lastName);
    }
}

let message = "hello there !"
var x = getFavoriteNUmber();

// Type Aliases

type Point = {
    x: number;
    y: number;
};

function Pin(pt: Point) {
    console.log(`X is : ${pt.x}`);
    console.log(`Y is : ${pt.y}`);
}

Pin({ x: 100, y: 200 });

interface IPoint {
    x: number,
    y: number
}

function Pin1(pt: IPoint) {
    console.log(`X is : ${pt.x}`);
    console.log(`Y is : ${pt.y}`);
}

Pin1({ x: 100, y: 200 });
