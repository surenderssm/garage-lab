use std::{fs::{OpenOptions}, io::Write};
fn main() {
    // args is a function in module env, which happens to be present in other module std

    // why `mut` : By default in rust, everything immutable

    let mut arguments = std::env::args().skip(1);
    // for arg in arguments {
    //     println!("Got arg : {}", arg);
    // }

    // Option string : Might be there or might not be
    // let key = arguments.next();
    // panicking, incase it doesn't exist
    let key = arguments.next().unwrap();
    let value = arguments.next().unwrap();

    let write_result = write_database(key, value);
    // pattern matching
    match write_result {
        Ok(()) => {
            // println!("it works");
        }
        Err(the_error) => {
            println!("We got an error {}", the_error);
        }
    }
}

// fn write_database(key: String, value: String) -> Result<(), std::io::Error> {
fn write_database(key: String, value: String) -> Result<(), std::io::Error> {
    // ! : macro not a function
    let content = format!("\n{} {}", key, value);
    //return fs::write("kv.db", content);
    // last line return is returned (without `;`)
    // fs::write("kv.db", content)
    let mut file = OpenOptions::new().append(true).open("kv.db").unwrap();
    return file.write_all(content.as_bytes());
}
