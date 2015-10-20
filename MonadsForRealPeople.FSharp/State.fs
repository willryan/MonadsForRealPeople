module State

open FsUnit.Xunit
open Xunit
open ExtCore.Control
open System

// setup
let randGen seed =
  let rand = new Random(seed)
  let nextValue = rand.NextDouble()
  let nextSeed = rand.Next()
  nextValue, nextSeed

// either monad

let stateful() = 
  let f = 
    state {
      let! rand1 = randGen
      let! rand2 = randGen
      let! rand3 = randGen
      return [rand1 ; rand2 ; rand3]
    } 
  fst <| f 500


// test
type ``test state``() =
  [<Fact>]
  member x.``state works``() =
    stateful() |> should equal [1. ; 2. ; 3.]

