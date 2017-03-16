open System.IO
open System.Text.RegularExpressions


let hello_mv (dir:DirectoryInfo) = 
    // find "hello.vm" file
    let hello_file = dir.GetFiles() |> 
                        Array.find(fun file -> file.Name = "hello.vm")

    let reg_you = @"^*.\byou\b.*$" // the regular expression to match each line with

    File.ReadAllLines(hello_file.FullName) |> // read every line
    Array.filter (fun line -> Regex.IsMatch(line, reg_you)) |> // filter out if does not contains word "you"
    Array.iter (fun line -> printfn "%s" line)  // print to stdout

    // set "hello.vm" and "hello.asm" paths
    let hello_vm_path = hello_file.FullName
    let hello_asm_path = dir.FullName + @"\hello.asm"

    // read all of the text from "hello.vm" and write to "hello.asm"
    let hello_text = File.ReadAllText(hello_file.FullName)  // read hello.vm as text
    File.WriteAllText(hello_asm_path, hello_text) // write text to hello.asm


let add_counter (dir:DirectoryInfo) = 
    // append to the end of each .vm file 
    // the index of that file in the array

    dir.GetFiles() 
    |> Array.filter (fun file -> file.Extension = ".vm")  // get all files with ".vm" extension
    |> Array.iteri (fun index file -> File.AppendAllText(file.FullName, index.ToString()))  // append index of file to file
    


[<EntryPoint>]
let main argv = 
    try 
        let dir = new DirectoryInfo(argv.[0]) // get directory files
        add_counter dir
        hello_mv dir
    with _ as ex -> printfn "ERROR: \n\n%s" (ex.ToString ())

    0 // EXIT_SUCCESS