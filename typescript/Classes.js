// Classes
class Point {
}
const pt = new Point();
pt.x = 100;
pt.y = 100;
// initialized classes
class Point1 {
    constructor() {
        this.x = 0;
        this.y = 0;
    }
    scale(n) {
        this.x *= n;
        this.y *= n;
    }
}
let pt1 = new Point1();
class Greeter {
    greet() {
        console.log("hi");
    }
    getName() {
        return "protected Hi";
    }
    getDna() {
        console.log("DNA");
    }
    static print() {
        console.log(Greeter.MyData);
    }
}
Greeter.MyData = 10;
const g = new Greeter();
g.greet();
