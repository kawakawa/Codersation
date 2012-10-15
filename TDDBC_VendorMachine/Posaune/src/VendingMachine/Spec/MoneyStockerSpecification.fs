﻿module MoneyStockerSpecification

open NaturalSpec

open VendingMachine

let pay_back (stocker:MoneyStocker) =
    printMethod ()
    stocker.PayBack() 

let inserted moneyArr (stocker:MoneyStocker) = 
    printMethod moneyArr
    moneyArr |> Seq.iter (fun m ->  stocker.Insert(new Money(m)))
    stocker

let coins (moneyArr:seq<MoneyKind>) (money:seq<Money>) = 
    printMethod moneyArr
    money |> Seq.sortBy (fun m -> m.Kind) 
          |> Seq.forall2 (fun m1 m2 -> m1 = m2.Kind) (moneyArr |> Seq.sort)

let stocked (moneyArr:seq<MoneyKind>) (stocker:MoneyStocker) = 
    printMethod moneyArr
    moneyArr |> Seq.iter (fun m -> stocker.Stock(new Money(m)))
    stocker

let used (usedAmount:int) (stocker:MoneyStocker) = 
    printMethod usedAmount
    stocker.TakeMoney(usedAmount)
    stocker
    

[<Scenario>]
let ``Given MoneyStocker Stocked 0 yen, when payback money you get 0 yen``() = 
    Given (new MoneyStocker())
        |> When pay_back 
        |> It should be empty
        |> Verify

[<Scenario>]
let ``Given MoneyStocker inserted one 100 yen coin, when payback money you get 100 yen coin``() = 
    Given (new MoneyStocker()) |> inserted [MoneyKind.Yen100]
        |> When pay_back 
        |> It should have (coins [MoneyKind.Yen100])
        |> Verify

[<Scenario>]
let ``Given MoneyStocker stocked 10 10yen coin, inserted one 200 yen coin and used 110yen, when payback money you get 10 yen coin``() = 
    Given (new MoneyStocker()) 
            |> stocked [for i in 1 .. 10 -> MoneyKind.Yen10] 
            |> inserted [MoneyKind.Yen100;MoneyKind.Yen100]
            |> used 110
        |> When pay_back 
        |> It should have (coins [for i in 1 .. 9 -> MoneyKind.Yen10])
        |> Verify