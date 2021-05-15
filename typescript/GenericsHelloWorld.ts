// Generics

// echo
function echo(arg: number): number {
    return arg;
}

function echo1(arg: any): any {
    return arg;
}

function echo2<Type>(arg: Type): Type {
    return arg;
}

let output = echo2<string>("hello");
// or 
let output1 = echo2("hello");

function echos<Type>(arg: Type[]): Type[] {
    console.log(arg.length);
    return arg;
}


let result = echos(new Array<number>());