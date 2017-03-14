open System.IO
open System.Text.RegularExpressions


(*A function that does:
1) Copies the content of 
file "hello.vm" to "hello.asm".
2) Prints every line in "hello.mv" 
that contains the work 'you'.*)
let hello_mv (dir:DirectoryInfo) = 
    // find "hello.vm" file
    let hello_file = dir.GetFiles() |> 
                        Array.find(fun file -> file.Name = "hello.vm")

    let reg_you = @"^*.\byou\b.*$" // the regular expression to match each line with

    // filter out all lines that do not
    // contain the word "you" and print 
    // the rest of the lines 
    File.ReadAllLines(hello_file.FullName) |> // read every line
    Array.filter (fun line -> Regex.IsMatch(line, reg_you)) |> // check if contains word "you"
    Array.iter (fun line -> printfn "%s" line)  // print to stdout

    // set "hello.vm" and "hello.asm" paths
    let hello_vm_path = hello_file.FullName
    let hello_asm_path = dir.FullName + @"\hello.asm"

    // read all of the text from "hello.vm" and write to "hello.asm"
    let hello_text = File.ReadAllText(hello_file.FullName)  // read hello.vm as text
    File.WriteAllText(hello_asm_path, hello_text) // write text to hello.asm


(*A function to add an incrementing value
to the end of each file with .vm extension*)
let add_counter (dir:DirectoryInfo) = 
    // append to the end of each .vm file 
    // the index of that file in the array

    dir.GetFiles() 
    |> Array.filter (fun file -> file.Extension = ".vm")  // get all files with ".vm" extension
    |> Array.iteri (fun index file -> File.AppendAllText(file.FullName, index.ToString()))
    


[<EntryPoint>]
let main argv = 
    try 
        let dir = new DirectoryInfo(argv.[0]) // get directory files
        add_counter dir
        hello_mv dir
    with _ as ex -> printfn "ERROR: \n\n%s" (ex.ToString ())

    0 // EXIT_SUCCESS