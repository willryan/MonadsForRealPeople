﻿module EitherChoice

open FsUnit.Xunit
open Xunit
open ExtCore.Control

// setup
let mightFail v =
  if v > 3 then Choice.result ((v - 3).ToString()) else Choice.failwith "too small"

let mightAlsoFail v =
  match v with
  | "1" -> Choice.result [1]
  | "2" -> Choice.result [1 ; 2]
  | "3" -> Choice.result [1 ; 2 ; 3]
  | _ -> Choice.failwith "too big"

let couldFailToo v =
  match v with
  | [1 ; 2] -> Choice.result "OneTwoPunch"
  | _ -> Choice.failwith "Knockout"

let finalComputationOn x y z =
  sprintf "%A %A %A" x y z

// either monad

let eitherEither() : Choice<string,string> = 
  choice {
    let! x = mightFail 5
    let! y = mightAlsoFail x
    let! z = couldFailToo y
    return finalComputationOn x y z
  }

// test
type ``test either``() =
  [<Fact>]
  member x.``either works``() =
    eitherEither() |> should equal "Some 2 [1 ; 2] OneTwoPunch"