﻿module State

open FsUnit
open NUnit.Framework
open ExtCore.Control
open System

// setup
let randGen maxValue seed =
  let rand = new Random(seed)
  let nextValue = rand.NextDouble() * maxValue
  let nextSeed = rand.Next()
  nextValue, nextSeed

// either monad

let stateful() = 
  let f = 
    state {
      let! rand1 = randGen 10.0
      let! rand2 = randGen 25.0
      let! rand3 = randGen 400.0
      return [rand1 ; rand2 ; rand3]
    } 
  fst <| f 500


// test
[<TestFixture>]
type ``test state``() =
  [<Test>]
  member x.``state works``() =
    stateful() |> should equal [1. ; 2. ; 3.]

