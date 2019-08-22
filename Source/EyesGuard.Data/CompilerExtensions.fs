module CompilerExtensions

open System.IO
open System.Runtime.CompilerServices
open System.Runtime.InteropServices

type CompilerInfo() =
    static member CompilerFilePath ([<CallerFilePath;Optional;DefaultParameterValue("")>]path:string) = path
    static member CompilerDirectory = CompilerInfo.CompilerFilePath() |> Path.GetDirectoryName
