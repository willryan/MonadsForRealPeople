module MaybeOption

open ExtCore.Control
open FsUnit
open NUnit.Framework

// setup
let mightReturnNothing v =
  if v > 3 then Some ((v - 3).ToString()) else None

let mightAlsoReturnNothing v =
  match v with
  | "1" -> Some [1]
  | "2" -> Some [1 ; 2]
  | "3" -> Some [1 ; 2 ; 3]
  | _ -> None

let couldBeNothingToo v =
  match v with
  | [1 ; 2] -> Some "OneTwoPunch"
  | _ -> None

let finalComputationOn x y z =
  sprintf "%s %A %s" x y z

// maybe monad

let maybeDoStuff() =
  maybe {
    let! x = mightReturnNothing 5
    let! y = mightAlsoReturnNothing x
    let! z = couldBeNothingToo y
    return finalComputationOn x y z
  }

// test
[<TestFixture>]
type ``test maybe``() =
  [<Test>]
  member x.``maybe works``() =
    maybeDoStuff() |> should equal (Some "2 [1; 2] OneTwoPunch")
