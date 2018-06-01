# ACE! NEW! FINE?

Welcome to ACE! NEW! FINE?

What is 'ACE! NEW! FINE?'? It is a 'brand new' (tm, full literature review not completed!) algorithm which incorperates a ton of techniques talked about this week. It stands for:

"Adversarial Co-Evolution of Neuro-Evolution with Feasible-Infeasible Evolution"

Yeah ... that is a lot of buzz words and a lot of the uses of "evolution". This was 100% because we fell it accurately explains our algrothm and not at all because it spells a few words. 

Let's break down the algorithm

## Ace Curriculm Learning
Let's say you have a complicated problem, for example driving a car (no idea where we got this inspriation from...), and you want to train your agents to be good at it  but the problem is, like, really hard. So you want to train it is solve a simplier problem first right? Great, so you can just design some simplier tracks and everyone is happy. But what if you are lazy like we are and you don't want to bother designing these easy tracks? Perhaps it would be amazing if you could design an agent to generate tracks which are *always* a little harder than the AI driving the cars would really like. Sounds good right? This is the motivation for ACE! NEW! FINE?

This is implmented by developing a fitness function for the track AI based on the performance of all cars and a fitness function for the car AI based on the performace of that car on all tracks. Formally:

Ft = 1/Nc * SUM(Progress(ci,t) + 1-Time(ci,t))
Fc = 1/Nt * SUM(Progress(c,ti) + Time(c,ti))

Where Progress and Time are values between 0-1. Progress tracks how far along the track the car got and Time tracks for fast they completed the track (or 0 if incomplete).




## NEAT Car Diving

## Evolving tracks (FI2POP)