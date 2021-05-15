// Classes

class Point {
    x: number;
    y: number;
}

const pt = new Point();
pt.x = 100;
pt.y = 100;

// initialized classes

class Point1 {
    x = 0;
    y = 0;

    scale(n: number): void {
        this.x *= n;
        this.y *= n;
    }
}

let pt1 = new Point1();


class Greeter {

    static MyData = 10;
    public greet() {
        console.log("hi");
    }

    protected getName() {
        return "protected Hi";
    }

    private getDna(){
        console.log("DNA");
    }

    static print(){
        console.log(Greeter.MyData);
    }
}

const g = new Greeter();
g.greet();