module Reader

open FsUnit.Xunit
open Xunit
open ExtCore.Control
open System

// setup
type Name = Name of string
type PhoneNumber = PhoneNumber of string

let lookup name phonebook =
  Map.find name phonebook

let thebook =
  Map.ofList 
    [
      (Name "Jenny", PhoneNumber "616-161-9000")
      (Name "Alice", PhoneNumber "800-333-1289")
      (Name "Joe", PhoneNumber "231-770-8499")
      (Name "Bob", PhoneNumber "888-123-4567")
      (Name "Phil", PhoneNumber "608-812-4490")
    ]

let getSomeNumbers() =
  thebook
  |> reader {
    let! num1 = lookup <| Name "Joe"
    let! num2 = lookup <| Name "Jenny"
    let! num3 = lookup <| Name "Phil"
    return [num1, num2, num3]
  } 

// test
type ``test reader``() =
  [<Fact>]
  member x.``reader works``() =
    getSomeNumbers() |> should equal 
      [
        PhoneNumber "231-770-8499"
        PhoneNumber "616-161-9000"
        PhoneNumber "608-812-4490"
      ]

